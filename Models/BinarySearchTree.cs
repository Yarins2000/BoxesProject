namespace Models
{
    public class BinarySearchTree<K, V> where K : IComparable<K>
    {
        public TreeNode<K, V> Root { get; set; }

        public BinarySearchTree()
        {
            Root = null;
        }
        public void AddNode(K data, V value)
        {
            if (Root == null)
                Root = new TreeNode<K, V>(data, value);
            else
                AddNode(data, value, Root);
        }
        private void AddNode(K data, V value, TreeNode<K, V> root)
        {
            if (data.CompareTo(root.Data) <= 0)
            {
                if (root.Left == null)
                    root.Left = new TreeNode<K, V>(data, value);
                else
                    AddNode(data, value, root.Left);
            }
            else
            {
                if (root.Right == null)
                    root.Right = new TreeNode<K, V>(data, value);
                else
                    AddNode(data, value, root.Right);
            }
        }

        public TreeNode<K, V> Get(K key)
        {
            if (Root == null)
                return null;
            return Get(key, Root);
        }
        private TreeNode<K, V> Get(K key, TreeNode<K, V> root)
        {
            if (root == null)
                return null;
            if (root.Data.Equals(key))
                return root;

            else if (key.CompareTo(root.Data) <= 0)
                return Get(key, root.Left);
            else
                return Get(key, root.Right);
        }

        public V GetValue(K key)
        {
            if (Root == null)
                return default;
            return GetValue(key, Root);
        }
        private V GetValue(K key, TreeNode<K, V> root)
        {
            if (root == null)
                return default;

            if (root.Data.Equals(key))
                return root.Value;
            else if (key.CompareTo(root.Data) <= 0)
                return GetValue(key, root.Left);
            else
                return GetValue(key, root.Right);
        }

        private void RemoveNodeWithoutChildren(TreeNode<K, V> deleteNode)
        {
            if (deleteNode.Left == null && deleteNode.Right == null)
            {
                var parent = GetParent(deleteNode.Data);
                if (parent != null)
                {
                    if (parent.Right == deleteNode)
                        parent.Right = null;
                    else
                        parent.Left = null;
                }
                else
                    this.Root = null;
            }
        }
        private void RemoveNodeWithOneChild(TreeNode<K, V> deleteNode)
        {
            var parent = GetParent(deleteNode.Data);
            if (parent != null)
            {
                if (deleteNode.Left != null && deleteNode.Right == null)
                {
                    if (parent.Right == deleteNode)   // if parent's RIGHT is the node we want to remove
                        parent.Right = deleteNode.Left;  // parent's RIGHT becomes the deleteNode LEFT
                    else
                        parent.Left = deleteNode.Left; // else parent's LEFT becomes the deleteNode LEFT
                    return;
                }

                else if (deleteNode.Left == null && deleteNode.Right != null)
                {
                    if (parent.Right == deleteNode)  // if parent's LEFT is the node we want to remove
                        parent.Right = deleteNode.Right; // parent's RIGHT becomes the deleteNode RIGHT
                    else
                        parent.Left = deleteNode.Right; // else parent's LEFT becomes the deleteNode RIGHT
                }
            }
            else //the deleted node is the root which doesn't have a parent
            {
                if(deleteNode.Left == null && deleteNode.Right != null) //the root has right child
                {
                    var minimum = GetMinimumNode(deleteNode.Right);
                    var minParent = GetParent(minimum.Data);
                    if(minParent != deleteNode)
                    {
                        minParent.Left = null;
                        if (minimum.Right != null)
                            minParent.Left = minimum.Right;
                    }
                    minimum.Right = deleteNode.Right;
                    this.Root = minimum;
                }
                else if(deleteNode.Left != null && deleteNode.Right == null)
                {
                    var maximum = GetMaximumNode(deleteNode.Left);
                    var maxParent = GetParent(maximum.Data);
                    if(maxParent != deleteNode)
                    {
                        maxParent.Right = null;
                        if(maximum.Left != null)
                            maxParent.Right = maximum.Left;
                    }
                    maximum.Left = deleteNode.Left;
                    this.Root = maximum;
                }
            }
        }
        private void RemoveNodeWithTwoChildren(TreeNode<K, V> deleteNode)
        {
            if (deleteNode.Left != null && deleteNode.Right != null)
            {
                var minimum = GetMinimumNode(deleteNode.Right);
                var parent = GetParent(deleteNode.Data);

                var minParent = GetParent(minimum.Data);  //find minimum's parent
                if (minParent != deleteNode)
                {
                    minParent.Left = null;
                    if (minimum.Right != null) // if minimum has a right node
                        minParent.Left = minimum.Right; // minimum's parent takes the node of the minimum's RIGHT node
                }
                minimum.Left = deleteNode.Left;    // minimum takes the RIGHT and the LEFT of the deleteNode
                if (deleteNode.Right != minimum)
                    minimum.Right = deleteNode.Right;

                if (parent is not null)
                {
                    if (parent.Right == deleteNode)   // connecting the node's parent with the minimum
                        parent.Right = minimum;
                    else
                        parent.Left = minimum;
                }
                else
                {
                    this.Root = minimum;
                }
            }
        }

        public void RemoveNode(K data)
        {
            var deleteNode = Get(data);
            if (deleteNode != null)
            {
                RemoveNodeWithoutChildren(deleteNode);
                RemoveNodeWithOneChild(deleteNode);
                RemoveNodeWithTwoChildren(deleteNode);
            }
        }

        public TreeNode<K, V> GetMinimumNode(TreeNode<K, V> t)
        {
            if (t.Left == null)
                return t;
            return GetMinimumNode(t.Left);
        }
        public TreeNode<K, V> GetMaximumNode(TreeNode<K, V> t)
        {
            if (t.Right == null)
                return t;
            return GetMaximumNode(t.Right);
        }

        /// <summary>
        /// Get the parent of the given value
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private TreeNode<K, V> GetParent(K data)
        {
            if (Root != null)
                return GetParent(data, Root);
            return null;
        }
        private TreeNode<K, V> GetParent(K data, TreeNode<K, V> t)
        {
            if (t == null)
                return null;

            if (t.Left != null && t.Left.Data.Equals(data))
                return t;
            else if (t.Right != null && t.Right.Data.Equals(data))
                return t;
            else if (data.CompareTo(t.Data) < 0)
                return GetParent(data, t.Left);
            else
                return GetParent(data, t.Right);
        }

        public void TraverseInOrder(Action<string> act)
        {
            TraverseInOrder(Root, act);
        }
        private void TraverseInOrder(TreeNode<K, V> root, Action<string> act)
        {
            if (root != null)
            {
                TraverseInOrder(root.Left, act);
                act($"{root.Data}");
                TraverseInOrder(root.Right, act);
            }
        }

        public bool IsExist(K data) => Get(data) is not null;
        public bool IsEmpty() => this.Root is null;

        public IEnumerable<TreeNode<K, V>> GetSuitableNodesByRange(K minData, K maxData) => GetSuitableNodesByRange(minData, maxData, Root);
        private IEnumerable<TreeNode<K, V>> GetSuitableNodesByRange(K minData, K maxData, TreeNode<K, V> root)
        {
            if (root is null)
                yield break;
            if (minData.CompareTo(root.Data) < 0)
                foreach (TreeNode<K, V> node in GetSuitableNodesByRange(minData, maxData, root.Left))
                    yield return node;
            if (minData.CompareTo(root.Data) <= 0 && maxData.CompareTo(root.Data) >= 0)
                yield return root;
            if (maxData.CompareTo(root.Data) > 0)
                foreach (TreeNode<K, V> node in GetSuitableNodesByRange(minData, maxData, root.Right))
                    yield return node;
        }

        public IEnumerable<TreeNode<K, V>> TraverseInOrderByEnumerator() => Enumerate(Root);
        private IEnumerable<TreeNode<K, V>> Enumerate(TreeNode<K, V> root)
        {
            if (root == null)
                yield break;
            foreach (var v in Enumerate(root.Left))
                yield return v;
            yield return root;
            foreach (var v in Enumerate(root.Right))
                yield return v;
        }
    }
}
