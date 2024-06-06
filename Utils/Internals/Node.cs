namespace GameUtils.Internals
{
    internal class Node<T>
    {
        public T Item { get; set; }

        public Node<T> Left { get; set; }

        public Node<T> Right { get; set; }

        public Node()
        {
        }
        public Node(T item)
        {
            Item = item;
        }
    }
}
