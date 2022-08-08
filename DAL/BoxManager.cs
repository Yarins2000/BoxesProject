using Models;
using System.Collections;

namespace DAL
{
    public class BoxManager
    {
        BinarySearchTree<double, BinarySearchTree<double, Box>> _storage;
        public BinarySearchTree<double, BinarySearchTree<double, Box>> Storage { get => _storage; }

        private const double PERCENTAGE_RANGE = 20;

        public BoxManager()
        {
            _storage = new();
        }

        public void AddNewBox(Box b)
        {
            if (_storage.IsExist(b.Length))
            {
                var innerTree = _storage.GetValue(b.Length);
                if (innerTree.IsExist(b.Height))
                {
                    var currentBox = innerTree.GetValue(b.Height);
                    currentBox.AddBoxCount(b.Quantity);
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

        public void RemoveBox(Box b)
        {
            var innerTree = _storage.GetValue(b.Length);
            innerTree.RemoveNode(b.Height);
        }

        public Box FindBox(double x, double y) => _storage.GetValue(x).GetValue(y);

        //public DoublyLinkedList<Box> GetSuitableBoxes(double maxX, double maxY)
        //{
        //    DoublyLinkedList<Box> boxList = new();
            
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
                    var innerTree = _storage.GetValue(x); //the inner tree of the correspond x
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