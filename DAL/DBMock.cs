using Models;
using Newtonsoft.Json;

namespace DAL
{
    public class DBMock
    {
        private static DBMock? _instance;
        public static DBMock Instance
        {
            get
            {
                if (_instance is null)
                    _instance = new DBMock();
                return _instance;
            }
        }
        public BinarySearchTree<double, BinarySearchTree<double, Box>> Tree { get; init; }
        public ListQueue<Box> BoxesDates { get; init; }
        private DBMock()
        {
            Tree = new();
            BoxesDates = new();
            Initialize();
        }

        private void Initialize()
        {
            Box b1 = new(4, 5, 7, new DateTime(2022, 8, 1));
            Box b1_1 = new(4, 4, 5, new DateTime(2022, 8, 2));
            Box b1_2 = new(4, 6, 5, new DateTime(2022, 8, 3));
            Box b2 = new(6.5, 2.5, 6, new DateTime(2022, 8, 4));
            Box b3 = new(8, 9, 5, new DateTime(2022, 8, 5));
            Box b4 = new(5.3, 4.8, 10, new DateTime(2022, 8, 6));
            Box b5 = new(8.9, 7, 11, new DateTime(2022, 8, 7));
            Box b6 = new(7.5, 5, 8, new DateTime(2022, 8, 8));
            Box b7 = new(8, 5, 8, new DateTime(2022, 8, 9));
            Box b8 = new(8, 12.5, 4, new DateTime(2022, 8, 10));
            Box b9 = new(8, 7, 6, new DateTime(2022, 8, 11));
            Box b10 = new(5.5, 5.5, 10, new DateTime(2022, 8, 12));
            Box b11 = new(5.3, 5, 6, new DateTime(2022, 8, 13));
            Box b11_1 = new(5.3, 7, 5, new DateTime(2022, 8, 14));
            Box b12 = new(4.2, 6, 10, new DateTime(2022, 8, 15));
            Box b12_1 = new(4.2, 5, 5, new DateTime(2022, 8, 16));
            Box b12_2 = new(4.2, 8, 9, new DateTime(2022, 8, 17));
            Box b13 = new(10, 5.2, 20, new DateTime(2022, 8, 18));

            AddNewBoxes(b1, b1_1, b1_2, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b11_1, b12, b12_1, b12_2, b13);
        }
        public void AddNewBox(Box newBox) //=========================
        {
            if (newBox is null || newBox.Quantity < 0) return;

            if (Tree.IsExist(newBox.Length))
            {
                var innerTree = Tree.GetValue(newBox.Length);
                if (innerTree.IsExist(newBox.Height))
                {
                    var currentBox = innerTree.GetValue(newBox.Height);
                    currentBox.AddBoxCount(newBox.Quantity);
                }
                else
                {
                    innerTree.AddNode(newBox.Height, newBox);
                    BoxesDates.Enqueue(newBox);
                }
            }
            else
            {
                var newInnerTree = new BinarySearchTree<double, Box>();
                newInnerTree.AddNode(newBox.Height, newBox);
                Tree.AddNode(newBox.Length, newInnerTree);
                BoxesDates.Enqueue(newBox);
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
