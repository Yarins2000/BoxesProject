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
        //change to internal later!!!
        public BinarySearchTree<double, BinarySearchTree<double, Box>> Tree { get; init; }
        public ListQueue<QNode<DateTime>> BoxesDates { get; init; }
        private DBMock()
        {
            Tree = new();
            BoxesDates = new();
            Initialize();
        }

        private void Initialize()
        {
            Box[] boxes = new Box[20];
            Box b1 = new(4, 5, 2);
            Box b1_1 = new(4, 4, 5);
            Box b1_2 = new(4, 6, 5);
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
            Box b11_1 = new(5.3, 7, 5);
            Box b12 = new(4.2, 6, 10);
            Box b12_1 = new(4.2, 5, 5);
            Box b12_2 = new(4.2, 8, 3);
            Box b13 = new(10, 5.2, 20);

            AddNewBoxes(b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b11_1, b12, b1_1, b1_2, b12_1, b12_2, b13);
        }
        //check later
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
