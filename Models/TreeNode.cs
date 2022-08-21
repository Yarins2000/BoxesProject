namespace Models
{
    public class TreeNode<K, V> where K : IComparable<K>
    {
        public K Data { get; private set; }
        public V Value { get; private set; }
        public TreeNode<K, V> Left { get; internal set; }
        public TreeNode<K, V> Right { get; internal set; }

        public TreeNode(K data, V value)
        {
            Data = data;
            Value = value;
            Left = null;
            Right = null;
        }
    }
}
