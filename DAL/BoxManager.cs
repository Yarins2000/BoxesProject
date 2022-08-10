using Models;
using System.Collections;

namespace DAL
{
    public class BoxManager
    {
        BinarySearchTree<double, BinarySearchTree<double, Box>> _storage;

        private readonly double _percentageRange = Configuration.Data.PercentageRange;
        private readonly int _maxQuantity = Configuration.Data.MaxQuantity;

        public BoxManager()
        {
            _storage = DBMock.Instance.Tree;
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
                    innerTree.AddNode(b.Height, b);
            }
            else
            {
                var newInnerTree = new BinarySearchTree<double, Box>();
                newInnerTree.AddNode(b.Height, b);
                _storage.AddNode(b.Length, newInnerTree);
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
        /// Get a binary tree with the suitable boxes(according to the given values)
        /// </summary>
        /// <param name="length"></param>
        /// <param name="maxLength"></param>
        /// <param name="height"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        //public BinarySearchTree<double, BinarySearchTree<double, Box>> GetSuitableBoxes(double length, double maxLength, double height, double maxHeight)
        //{
        //    var suitableTree = new BinarySearchTree<double, BinarySearchTree<double, Box>>();
        //    var xTreeEnumerator = _storage.GetSuitableNodesByRange(length, maxLength);
        //    foreach (var node in xTreeEnumerator)
        //    {
        //        suitableTree.AddNode(node.Data, new BinarySearchTree<double, Box>());
        //        if (node.Value is not null)
        //        {
        //            var innerXTreeEnumerator = node.Value.GetSuitableNodesByRange(height, maxHeight);
        //            foreach (var innerNode in innerXTreeEnumerator)
        //                suitableTree.GetValue(node.Data).AddNode(innerNode.Data, innerNode.Value);
        //                //get the current value, then add a new node with the innerNode data&value each loop(iterates through the suitable values in the node.Value)
        //        }
        //    } 
        //    return suitableTree;
        //}
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
        /// choose the best box choices for the gift.
        /// </summary>
        /// <param name="x">the required gift's length</param>
        /// <param name="y">the required gift's height</param>
        public void ChooseBoxesForGift(double x, double y, int desiredBoxCount)
        {
            double maxLength, maxHeight;
            MaximumBoxSize(x, y, out maxLength, out maxHeight);

            var suitableBoxesTree = GetSuitableBoxes(x, maxLength, y, maxHeight);
        }

        /*public string ChooseABoxForGift(double x, double y)
        {
            var wantedBox = FindBox(x, y);
            if (wantedBox is not null)
            {
                wantedBox.BuyABox();
                return wantedBox.ToString(); //change later
            }
            else
            {
                if (_storage.IsExist(x))
                {
                    var innerTree = _storage.GetValue(x); //the inner tree of the corresponded x
                    double maxSuitableSize = y + (y * PERCENTAGE_RANGE) / 100; //the max height that could be offered
                    var SmallestSuitableBox = innerTree.Root.Right is not null ?
                        innerTree.GetMinimumNode(innerTree.Root.Right).Value : null;
                    if (SmallestSuitableBox is not null && SmallestSuitableBox.Height >= maxSuitableSize)
                        SmallestSuitableBox.BuyABox();
                }
            }
            return "";
        }*/

    }
}