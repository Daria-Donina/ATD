namespace ATD.Trees;

public class BinarySearchTree<T> : ITree<T, BinarySearchTree<T>.Node>, IOrderTraversable<T>
{
    public class Node
    {
        public int Key { get; set; }
        public T Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(int key, T value)
        {
            Key = key;
            Value = value;
            Left = null;
            Right = null;
        }
    }

    public Node Root { get; private set; }

    public void Insert(int key, T value) =>
        Root = Insert(Root, key, value);

    public bool Lookup(int key, out T value)
    {
        var node = Lookup(Root, key);
        if (node != null)
        {
            value = node.Value;
            return true;
        }

        value = default;
        return false;
    }

    public void Remove(int key) => Root = Remove(Root, key);

    public IEnumerable<KeyValuePair<int, T>> InOrderTraversal() => 
        InOrderTraversal(Root);

    public IEnumerable<KeyValuePair<int, T>> PreOrderTraversal() => 
        PreOrderTraversal(Root);

    public IEnumerable<KeyValuePair<int, T>> PostOrderTraversal() => 
        PostOrderTraversal(Root);

    private Node Insert(Node node, int key, T value)
    {
        if (node == null)
            return new Node(key, value);

        if (key < node.Key)
            node.Left = Insert(node.Left, key, value);
        else if (key > node.Key)
            node.Right = Insert(node.Right, key, value);
        else
            node.Value = value;

        return node;
    }

    private Node Lookup(Node node, int key)
    {
        if (node == null || node.Key == key)
            return node;

        return Lookup(key < node.Key ? node.Left : node.Right, key);
    }

    private Node Remove(Node node, int key)
    {
        if (node == null)
            return null;

        if (key < node.Key)
            node.Left = Remove(node.Left, key);
        else if (key > node.Key)
            node.Right = Remove(node.Right, key);
        else
        {
            if (node.Left == null)
                return node.Right;
            else if (node.Right == null)
                return node.Left;
            else
            {
                Node successor = MinValueNode(node.Right);
                node.Key = successor.Key;
                node.Value = successor.Value;
                node.Right = Remove(node.Right, successor.Key);
            }
        }

        return node;
    }

    private Node MinValueNode(Node node)
    {
        Node current = node;
        while (current.Left != null)
            current = current.Left;

        return current;
    }

    private IEnumerable<KeyValuePair<int, T>> InOrderTraversal(Node node)
    {
        if (node == null)
            return new List<KeyValuePair<int, T>>();

        var nodes = new List<KeyValuePair<int, T>>();
        nodes.AddRange(InOrderTraversal(node.Left));
        nodes.Add(new KeyValuePair<int, T>(node.Key, node.Value));
        nodes.AddRange(InOrderTraversal(node.Right));

        return nodes;
    }

    private IEnumerable<KeyValuePair<int, T>> PreOrderTraversal(Node node)
    {
        if (node == null)
            return new List<KeyValuePair<int, T>>();

        var nodes = new List<KeyValuePair<int, T>> { new(node.Key, node.Value) };
        nodes.AddRange(PreOrderTraversal(node.Left));
        nodes.AddRange(PreOrderTraversal(node.Right));

        return nodes;
    }

    private IEnumerable<KeyValuePair<int, T>> PostOrderTraversal(Node node)
    {
        if (node == null)
            return new List<KeyValuePair<int, T>>();

        List<KeyValuePair<int, T>> nodes = new();
        nodes.AddRange(PostOrderTraversal(node.Left));
        nodes.AddRange(PostOrderTraversal(node.Right));
        nodes.Add(new(node.Key, node.Value));

        return nodes;
    }
}