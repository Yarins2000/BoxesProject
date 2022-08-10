using Models;
using System.Collections;

namespace DAL
{
    public class BoxManager
    {
        BinarySearchTree<double, BinarySearchTree<double, Box>> _storage;

        private const double PERCENTAGE_RANGE = 20;
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
            {
                AddNewBox(b);
            }
        }

        public void RemoveBox(Box b)
        {
            var innerTree = _storage.GetValue(b.Length);
            innerTree.RemoveNode(b.Height);
        }

        public Box FindBox(double x, double y) => _storage.GetValue(x).GetValue(y);

        //public DoublyLinkedList<Box> GetSuitableBoxes(double x, double maxX, double y, double maxY)
        //    => GetSuitableBoxes(x, maxX, y, maxY, root)
        //public DoublyLinkedList<Box> GetSuitableBoxes(double x, double maxX, double y, double maxY)
        //{
        //    var xTree = _storage.GetSuitableNodesByRange(x, maxX);
        //    var yTree = _storage.GetSuitableNodesByRange(y, maxY);

        //}

        public string ChooseABoxForGift(double x, double y)
        {
            var wantedBox = FindBox(x, y);
            if(wantedBox is not null)
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
        }

    }
}