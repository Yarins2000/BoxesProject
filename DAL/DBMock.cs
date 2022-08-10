using Models;
using Newtonsoft.Json;

namespace DAL
{
    public class DBMock
    {
        private static DBMock _instance;
        public static DBMock Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DBMock();
                return _instance;
            }
        }
        public BinarySearchTree<double, BinarySearchTree<double, Box>> Tree { get; init; }
        private DBMock()
        {
            Tree = new();
            Initialize();
        }

        private void Initialize()
        {
            Box[] boxes = new Box[20];
            Box b1 = new(4, 5, 2);
            Box b2 = new(6.5, 2.5, 6);
            Box b3 = new(8, 9, 5);
            Box b4 = new(5.3, 4.8, 10);
            Box b5 = new(8.9, 7, 11);
            Box b6 = new(7.5, 5, 3);
            Box b7 = new(8, 5, 3);
            Box b8 = new(8, 12.5, 4);
            Box b9 = new(8, 7, 6);
            Box b10 = new(5.5, 5.5, 10);
            Box b11 = new(5.3, 5, 2);

            AddNewBoxes(b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11);
        }
        public void AddNewBox(Box b)
        {
            if (b is null || b.Quantity < 0) return;

            if (Tree.IsExist(b.Length))
            {
                var innerTree = Tree.GetValue(b.Length);
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
                Tree.AddNode(b.Length, newInnerTree);
            }
        }
        public void AddNewBoxes(params Box[] boxes)
        {
            foreach (var b in boxes)
            {
                AddNewBox(b);
            }
        }
    }
}
