using Models;

namespace DAL
{
    public class BoxManager
    {
        /// <summary>
        /// The tree from the DB.
        /// </summary>
        BinarySearchTree<double, BinarySearchTree<double, Box>> _storage;
        /// <summary>
        /// The box queue from the DB.
        /// </summary>
        ListQueue<Box> _boxesQueue;

        private readonly double _percentageRange = Configuration.Data.PercentageRange;
        private readonly int _maxQuantity = Configuration.Data.MaxQuantity;
        private readonly int _minQuantityToAlert = Configuration.Data.MinQuantityToAlert;
        private readonly int _maxDaysUntilExpiration = Configuration.Data.MaxDaysUntilExpiration;

        public BoxManager()
        {
            _storage = DBMock.Instance.Tree;
            _boxesQueue = DBMock.Instance.BoxesDates;
        }

        /// <summary>
        /// Adds a new box to the tree.
        /// </summary>
        /// <param name="newBox">The new added box</param>
        public void AddNewBox(Box newBox)
        {
            if (newBox is null || newBox.Quantity < 0) return;

            if (_storage.IsExist(newBox.Length))
            {
                var innerTree = _storage.GetValue(newBox.Length);
                if (innerTree.IsExist(newBox.Height))
                {
                    var currentBox = innerTree.GetValue(newBox.Height);
                    currentBox.AddBoxCount(newBox.Quantity);
                    currentBox.Quantity = currentBox.Quantity > _maxQuantity ? _maxQuantity : currentBox.Quantity;
                }
                else
                {
                    newBox.UpdateTheDate();
                    innerTree.AddNode(newBox.Height, newBox);
                    _boxesQueue.Enqueue(newBox);
                }
            }
            else
            {
                var newInnerTree = new BinarySearchTree<double, Box>();
                newBox.Quantity = newBox.Quantity > _maxQuantity ? _maxQuantity : newBox.Quantity;
                newInnerTree.AddNode(newBox.Height, newBox);
                _storage.AddNode(newBox.Length, newInnerTree);
                _boxesQueue.Enqueue(newBox);
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
            _boxesQueue.Dequeue(b);
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
        /// Gets a list with the suitable boxes (according to the given values).
        /// </summary>
        /// <param name="length">the boxes minimum length (the best result)</param>
        /// <param name="maxLength">the boxes maximum length (according to a specific percentage range)</param>
        /// <param name="height">the boxes minimum height (the best result)</param>
        /// <param name="maxHeight">the boxes maximum height (according to a specific percentage range)</param>
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
        /// Recieves the suitable list of boxes(from the <see cref="GetSuitableBoxes(double, double, double, double"/> function) and takes the 'amount' best of them.
        /// </summary>
        /// <param name="length">the required gift length</param>
        /// <param name="height">the required gift height</param>
        /// <param name="requiredBoxAmount">the demanded amount that the user asked</param>
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
        /// 
        /// </summary>
        /// <returns>True if the box is about to be out of stock (according to a minimum amount the alerts)</returns>
        public bool IsBoxQuantityAlmostEmpty(Box b) => b.Quantity <= _minQuantityToAlert && b.Quantity > 0;

        /// <summary>
        /// Occurs after the user agreed to make the purchasing.
        /// </summary>
        /// <param name="list">The list of boxes that would be deleted from the storage</param>
        public void UpdateStorageAfterPurchase(DoublyLinkedList<Box> list)
        {
            foreach (var box in list)
            {
                box.Quantity -= box.AmountToGive;
                box.AmountToGive = 0;
                if (box.IsEmpty())
                    RemoveBox(box);
                else
                {
                    box.UpdateTheDate();
                    _boxesQueue.Dequeue(box);
                    _boxesQueue.Enqueue(box);
                }
            }
        }

        /// <summary>
        /// Show all the boxes from the storage.
        /// </summary>
        public void ShowAllBoxes(Action<string> act)
        {
            var allNodes = _storage.TraverseInOrderByEnumerator();
            foreach (var node in allNodes)
            {
                if (node.Value is not null)
                {
                    var innerAllNodes = _storage.GetValue(node.Data).TraverseInOrderByEnumerator();
                    foreach (var innerNode in innerAllNodes)
                        act(innerNode.Value.ToString());
                }
                act("");
            }
        }

        /// <summary>
        /// Removes the expired boxes (boxes that haven't been bought for a period of time that was determined).
        /// </summary>
        public void DeleteExpiredBoxes(Action<string> act)
        {
            foreach (var box in _boxesQueue)
            {
                var currentBoxDate = box.UpdatedDate;
                if (currentBoxDate.AddDays(_maxDaysUntilExpiration) < DateTime.Now)
                {
                    act("Box: " + box.ToString() + "\nhas been removed from stock since it's expired.");
                    RemoveBox(box);
                    _boxesQueue.Dequeue(box);
                }
            }
        }
    }
}