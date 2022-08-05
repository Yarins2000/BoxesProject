using Models;
namespace DAL
{
    public class BoxManager
    {
        BinarySearchTree<double, BinarySearchTree<double, Box>> _storage;
        public BinarySearchTree<double, BinarySearchTree<double, Box>> Storage { get => _storage; }

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
                    currentBox.Quantity += b.Quantity;
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
            _storage.RemoveNode(b.Length);
        }
    }
}