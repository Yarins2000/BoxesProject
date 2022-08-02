namespace Models
{
    internal class TreeNode<K, V> where K : IComparable<K>
    {
        public K? Data { get; set; }
        public V? Value { get; set; }
        public TreeNode<K, V>? Left { get; set; }
        public TreeNode<K, V>? Right { get; set; }

        public TreeNode(K data, V value)
        {
            Data = data;
            Value = value;
            Left = null;
            Right = null;
        }
    }
}
