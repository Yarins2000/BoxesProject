using Models;
using System.Collections;

namespace DAL
{
    public class BoxManager
    {
        BinarySearchTree<double, BinarySearchTree<double, Box>> _storage;
        ListQueue<QNode<DateTime>> _boxesDates;

        private readonly double _percentageRange = Configuration.Data.PercentageRange;
        private readonly int _maxQuantity = Configuration.Data.MaxQuantity;

        public BoxManager()
        {
            _storage = DBMock.Instance.Tree;
            _boxesDates = DBMock.Instance.BoxesDates;
        }

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

        public void RemoveBox(Box b)
        {
            var innerTree = _storage.GetValue(b.Length);
            innerTree.RemoveNode(b.Height);
            if (innerTree.IsEmpty())
                _storage.RemoveNode(b.Length);
        }

        public Box FindBox(double x, double y) => _storage.GetValue(x).GetValue(y);

        /// <summary>
        /// Get a doubly linked list with the suitable boxes(according to the given values)
        /// </summary>
        /// <param name="length"></param>
        /// <param name="maxLength"></param>
        /// <param name="height"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
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

        public void MaximumBoxSize(double x, double y, out double maxLength, out double maxHeight)
        {
            maxLength = x + x * _percentageRange / 100;
            maxHeight = y + y * _percentageRange / 100;
        }

        /// <summary>
        /// Recieves the suitable list of boxes(according to the demands) 
        /// </summary>
        /// <param name="length"></param>
        /// <param name="height"></param>
        /// <param name="desiredBoxAmount"></param>
        /// <param name="AreThereEnoughBoxes">represents wether there are enough boxes for the gift</param>
        /// <returns>A list of boxes according to the demanded amount of boxes for the gift</returns>
        public DoublyLinkedList<Box> SuitableBoxListByAmount(double length, double height, int desiredBoxAmount, out bool AreThereEnoughBoxes)
        {
            MaximumBoxSize(length, height, out double maxLength, out double maxHeight);

            var suitableBoxesList = GetSuitableBoxes(length, maxLength, height, maxHeight); //represents a list of all the suitable boxes(according to the given size) from the tree
            var suitableBoxesByAmount = new DoublyLinkedList<Box>(); //represents a list of boxes of the most suitable boxes(by the given size) according to the given amount
            foreach (var box in suitableBoxesList)
            {
                if (desiredBoxAmount is 0)
                    break;
                if (box.Quantity >= desiredBoxAmount)
                {
                    box.AmountToGive += desiredBoxAmount;
                    suitableBoxesByAmount.AddToEnd(box);
                }
                else
                {
                    box.AmountToGive += box.Quantity;
                    suitableBoxesByAmount.AddToEnd(box);
                }
                desiredBoxAmount -= box.AmountToGive;
            }
            AreThereEnoughBoxes = desiredBoxAmount is 0;
            return suitableBoxesByAmount;
        }

        /// <summary>
        /// Occurs after the user agreed to make the purchasing
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