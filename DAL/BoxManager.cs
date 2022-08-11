using Models;
using System.Collections;

namespace DAL
{
    public class BoxManager
    {
        /// <summary>
        /// The tree from the DB.
        /// </summary>
        BinarySearchTree<double, BinarySearchTree<double, Box>> _storage;
        /// <summary>
        /// The dates queue from the DB.
        /// </summary>
        ListQueue<QNode<DateTime>> _boxesDates;

        private readonly double _percentageRange = Configuration.Data.PercentageRange;
        private readonly int _maxQuantity = Configuration.Data.MaxQuantity;

        public BoxManager()
        {
            _storage = DBMock.Instance.Tree;
            _boxesDates = DBMock.Instance.BoxesDates;
        }

        /// <summary>
        /// Adds a new box to the tree.
        /// </summary>
        /// <param name="b">The new added box</param>
        public void AddNewBox(Box b)
        {
            if (b is null || b.Quantity < 0) return;

            if (_storage.IsExist(b.Length))
            {
                var innerTree = _storage.GetValue(b.Length);
                if (innerTree.IsExist(b.Height))
                {
                    var currentBox = innerTree.GetValue(b.Height);
                    currentBox.AddBoxCount(b.Quantity);
                    currentBox.Quantity = currentBox.Quantity > _maxQuantity ? _maxQuantity : currentBox.Quantity;
                }
                else
                {
                    innerTree.AddNode(b.Height, b);
                    _boxesDates.Enqueue(b.DateReference);
                }
            }
            else
            {
                var newInnerTree = new BinarySearchTree<double, Box>();
                newInnerTree.AddNode(b.Height, b);
                _storage.AddNode(b.Length, newInnerTree);
                _boxesDates.Enqueue(b.DateReference);
            }
        }
        public void AddNewBoxes(params Box[] boxes)
        {
            foreach (var b in boxes)
                AddNewBox(b);
        }

        /// <summary>
        /// Removes a node according to the given box.
        /// </summary>
        /// <param name="b">the box that is in the removed node</param>
        public void RemoveBox(Box b)
        {
            var innerTree = _storage.GetValue(b.Length);
            innerTree.RemoveNode(b.Height);
            if (innerTree.IsEmpty())
                _storage.RemoveNode(b.Length);
        }

        /// <summary>
        /// Finds a box in the tree according to the given size values.
        /// </summary>
        /// <param name="length">the box's length</param>
        /// <param name="height">the box's height</param>
        /// <returns>The box with the given values.</returns>
        public Box FindBox(double length, double height) => _storage.GetValue(length).GetValue(height);

        /// <summary>
        /// Gets a list with the suitable boxes(according to the given values).
        /// </summary>
        /// <param name="length">the boxes minimum length(the best result)</param>
        /// <param name="maxLength">the boxes maximum length(according to a specific percentage range)</param>
        /// <param name="height">the boxes minimum height(the best result)</param>
        /// <param name="maxHeight">the boxes maximum height(according to a specific percentage range)</param>
        /// <returns>List with the suitable boxes from the storage.</returns>
        public DoublyLinkedList<Box> GetSuitableBoxes(double length, double maxLength, double height, double maxHeight)
        {
            var suitableList = new DoublyLinkedList<Box>();
            var xTreeEnumerator = _storage.GetSuitableNodesByRange(length, maxLength);
            foreach (var node in xTreeEnumerator)
            {
                if (node.Value is not null)
                {
                    var innerXTreeEnumerator = node.Value.GetSuitableNodesByRange(height, maxHeight);
                    foreach (var innerNode in innerXTreeEnumerator)
                        if (innerNode.Value is not null)
                            suitableList.AddToEnd(innerNode.Value);
                }
            }
            return suitableList;
        }

        /// <summary>
        /// Calculates the maximum top range of the given length and height. 
        /// </summary>
        public void MaximumBoxSize(double length, double height, out double maxLength, out double maxHeight)
        {
            maxLength = length + length * _percentageRange / 100;
            maxHeight = height + height * _percentageRange / 100;
        }

        /// <summary>
        /// Recieves the suitable list of boxes(from the <see cref="GetSuitableBoxes(double, double, double, double"/> function) and takes the 'amount' best of them.
        /// </summary>
        /// <param name="length">the required gift length</param>
        /// <param name="height">the required gift height</param>
        /// <param name="requiredBoxAmount"></param>
        /// <param name="AreThereEnoughBoxes">represents wether there are enough boxes for the gift or not</param>
        /// <returns>A list of boxes according to the demanded amount for the gifts.</returns>
        public DoublyLinkedList<Box> SuitableBoxListByAmount(double length, double height, int requiredBoxAmount, out bool AreThereEnoughBoxes)
        {
            MaximumBoxSize(length, height, out double maxLength, out double maxHeight);

            var suitableBoxesList = GetSuitableBoxes(length, maxLength, height, maxHeight); //represents a list of all the suitable boxes(according to the given size) from the tree
            var suitableBoxesByAmount = new DoublyLinkedList<Box>(); //represents a list of boxes of the most suitable boxes(by the given size) according to the given amount
            foreach (var box in suitableBoxesList)
            {
                if (requiredBoxAmount is 0)
                    break;
                if (box.Quantity >= requiredBoxAmount)
                {
                    box.AmountToGive += requiredBoxAmount;
                    suitableBoxesByAmount.AddToEnd(box);
                }
                else
                {
                    box.AmountToGive += box.Quantity;
                    suitableBoxesByAmount.AddToEnd(box);
                }
                requiredBoxAmount -= box.AmountToGive;
            }
            AreThereEnoughBoxes = requiredBoxAmount is 0;
            return suitableBoxesByAmount;
        }

        /// <summary>
        /// Occurs after the user agreed to make the purchasing.
        /// </summary>
        /// <param name="list">The list of boxes to delete from the storage</param>
        public void UpdateTreeAfterPurchase(DoublyLinkedList<Box> list)
        {
            foreach (var box in list)
            {
                box.Quantity -= box.AmountToGive;
                box.AmountToGive = 0;
                if (box.Quantity <= 0)
                    RemoveBox(box);
                else
                {
                    //update the box.DateReference to current date and put it last in the queue
                }
            }
        }

        //inorder of the tree - complete later!
        public string Show() => "";
    }
}