namespace Models
{
    public class BinarySearchTree<K, V> where K : IComparable<K>
    {
        private TreeNode<K, V> Root { get; set; }

        public BinarySearchTree()
        {
            Root = null;
        }

        /// <summary>
        /// Adds a new node to the tree.
        /// </summary>
        /// <param name="data">the node's data</param>
        /// <param name="value">the node's value</param>
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

        /// <summary>
        /// Gets the corresponded node by the given data.
        /// </summary>
        /// <param name="data">the searched node's data</param>
        private TreeNode<K, V> GetNode(K data)
        {
            if (Root == null)
                return null;
            return GetNode(data, Root);
        }
        private TreeNode<K, V> GetNode(K data, TreeNode<K, V> root)
        {
            if (root == null)
                return null;
            if (root.Data.Equals(data))
                return root;

            else if (data.CompareTo(root.Data) <= 0)
                return GetNode(data, root.Left);
            else
                return GetNode(data, root.Right);
        }

        /// <summary>
        /// Gets the corresponded node's value according to the given data.
        /// </summary>
        /// <param name="data">The searched node's value's data</param>
        public V GetValue(K data)
        {
            if (Root == null)
                return default;
            return GetValue(data, Root);
        }
        private V GetValue(K data, TreeNode<K, V> root)
        {
            if (root == null)
                return default;

            if (root.Data.Equals(data))
                return root.Value;
            else if (data.CompareTo(root.Data) <= 0)
                return GetValue(data, root.Left);
            else
                return GetValue(data, root.Right);
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
                if(deleteNode.Left == null && deleteNode.Right != null) //the root has a right child
                {
                    var minimum = GetMinimumNode(deleteNode.Right);
                    var minParent = GetParent(minimum.Data);
                    if(minParent != deleteNode)
                    {
                        minParent.Left = null;
                        if (minimum.Right != null)
                            minParent.Left = minimum.Right;
                    }
                    if(deleteNode.Right != minimum)
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
                    if(deleteNode.Left != maximum)
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

                if (parent is not null) // if the removed node is not the root
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

        /// <summary>
        /// Removes the node according to the given data.
        /// </summary>
        /// <param name="data">the removed node's data</param>
        public void RemoveNode(K data)
        {
            var deleteNode = GetNode(data);
            if (deleteNode != null)
            {
                RemoveNodeWithoutChildren(deleteNode);
                RemoveNodeWithOneChild(deleteNode);
                RemoveNodeWithTwoChildren(deleteNode);
            }
        }

        /// <summary>
        /// Gets the minimum node.
        /// </summary>
        /// <param name="t">the node that is given in order to find its minimum node(that is greater than it)</param>
        private TreeNode<K, V> GetMinimumNode(TreeNode<K, V> t)
        {
            if (t.Left == null)
                return t;
            return GetMinimumNode(t.Left);
        }
        /// <summary>
        /// Gets the maximum node.
        /// </summary>
        /// <param name="t">the node that is given in order to find its maximum node(that is smaller than it)</param>
        private TreeNode<K, V> GetMaximumNode(TreeNode<K, V> t)
        {
            if (t.Right == null)
                return t;
            return GetMaximumNode(t.Right);
        }

        /// <summary>
        /// Gets the parent of the given node
        /// </summary>
        /// <param name="data">the data of the child's node</param>
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


        /// <summary>
        /// If the node exists return true, otherwise false.
        /// </summary>
        /// <param name="data">the data of the examinated node</param>
        public bool IsExist(K data) => GetNode(data) is not null;

        /// <summary>
        /// True if the tree is empty (its root is null), otherwise false.
        /// </summary>
        public bool IsEmpty() => this.Root is null;

        /// <summary>
        /// Gets the suitable nodes by a K-size(data) range.
        /// </summary>
        /// <param name="minData">the minimum required data of the nodes</param>
        /// <param name="maxData">the maximum required data of the nodes</param>
        /// <returns>An enumerable that would be iterated and returns the suitable nodes.</returns>
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

        /// <summary>
        /// Gets all the nodes of the tree.
        /// </summary>
        /// <returns>An Enumerable that would be iterated and yields the tree nodes.</returns>
        public IEnumerable<TreeNode<K, V>> TraverseInOrderByEnumerator() => EnumerateThroughTree(Root);
        private IEnumerable<TreeNode<K, V>> EnumerateThroughTree(TreeNode<K, V> root)
        {
            if (root == null)
                yield break;
            foreach (var v in EnumerateThroughTree(root.Left))
                yield return v;
            yield return root;
            foreach (var v in EnumerateThroughTree(root.Right))
                yield return v;
        }
    }
}
