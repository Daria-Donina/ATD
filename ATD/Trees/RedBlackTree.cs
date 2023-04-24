using System.Drawing;

namespace ATD.Trees;

public class RedBlackTree<T> : ITree<T, RedBlackTree<T>.Node>, IOrderTraversable<T>
{
    public class Node
    {
        public int Key { get; }
        public T Value { get; set; }
        public Color Color { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Parent { get; set; }

        public Node(int key, T value)
        {
            Key = key;
            Value = value;
            Color = Color.Red;
            Left = null;
            Right = null;
            Parent = null;
        }
    }

    private readonly Node _nullNode;

    public RedBlackTree()
    {
        _nullNode = new Node(0, default);
        _nullNode.Color = Color.Black;
        Root = _nullNode;
    }

    public Node Root { get; private set; }

    public void Insert(int key, T value)
    {
        Node newNode = new Node(key, value);
        Node freeNode = _nullNode;
        Node current = Root;
        while (current != _nullNode)
        {
            freeNode = current;
            if (current.Key == newNode.Key)
            {
                current.Value = value;
                return;
            }
            
            current = newNode.Key < current.Key ? current.Left : current.Right;
        }

        newNode.Parent = freeNode;
        if (freeNode == _nullNode)
            Root = newNode;
        else if (newNode.Key < freeNode.Key)
            freeNode.Left = newNode;
        else
            freeNode.Right = newNode;

        newNode.Left = _nullNode;
        newNode.Right = _nullNode;
        newNode.Color = Color.Red;
        InsertFixup(newNode);
    }

    public bool Lookup(int key, out T value)
    {
        var node = Search(key);

        if (node == null)
        {
            value = default;
            return false;
        }

        value = node.Value;
        return true;
    }

    public void Remove(int key)
    {
        var foundNode = Search(key);
        if (foundNode == null)
            return;

        Node x;
        Node y = foundNode;
        Color yOriginalColor = y.Color;
        if (foundNode.Left == _nullNode)
        {
            x = foundNode.Right;
            Transplant(foundNode, foundNode.Right);
        }
        else if (foundNode.Right == _nullNode)
        {
            x = foundNode.Left;
            Transplant(foundNode, foundNode.Left);
        }
        else
        {
            y = Minimum(foundNode.Right);
            yOriginalColor = y.Color;
            x = y.Right;
            if (y.Parent == foundNode)
            {
                x.Parent = y;
            }
            else
            {
                Transplant(y, y.Right);
                y.Right = foundNode.Right;
                y.Right.Parent = y;
            }

            Transplant(foundNode, y);
            y.Left = foundNode.Left;
            y.Left.Parent = y;
            y.Color = foundNode.Color;
        }

        if (yOriginalColor == Color.Black)
        {
            DeleteFixup(x);
        }
    }
    
    public IEnumerable<KeyValuePair<int, T>> InOrderTraversal() => 
        InOrderTraversal(Root);

    public IEnumerable<KeyValuePair<int, T>> PreOrderTraversal() => 
        PreOrderTraversal(Root);

    public IEnumerable<KeyValuePair<int, T>> PostOrderTraversal() => 
        PostOrderTraversal(Root);

    private void LeftRotate(Node node)
    {
        var rightNode = node.Right;
        node.Right = rightNode.Left;
        if (rightNode.Left != _nullNode)
            rightNode.Left.Parent = node;

        rightNode.Parent = node.Parent;
        if (node.Parent == _nullNode)
            Root = rightNode;
        else if (node == node.Parent.Left)
            node.Parent.Left = rightNode;
        else
            node.Parent.Right = rightNode;

        rightNode.Left = node;
        node.Parent = rightNode;
    }

    private void RightRotate(Node node)
    {
        var leftNode = node.Left;
        node.Left = leftNode.Right;
        if (leftNode.Right != _nullNode)
            leftNode.Right.Parent = node;

        leftNode.Parent = node.Parent;
        if (node.Parent == _nullNode)
            Root = leftNode;
        else if (node == node.Parent.Right)
            node.Parent.Right = leftNode;
        else
            node.Parent.Left = leftNode;

        leftNode.Right = node;
        node.Parent = leftNode;
    }

    private Node Search(int key)
    {
        Node current = Root;
        while (current != _nullNode)
        {
            if (key == current.Key)
                return current;

            current = key < current.Key ? current.Left : current.Right;
        }

        return null;
    }

    private void InsertFixup(Node newNode)
    {
        while (newNode.Parent.Color == Color.Red)
        {
            if (newNode.Parent == newNode.Parent.Parent.Left)
            {
                Node rightnode = newNode.Parent.Parent.Right;
                if (rightnode.Color == Color.Red)
                {
                    newNode.Parent.Color = Color.Black;
                    rightnode.Color = Color.Black;
                    newNode.Parent.Parent.Color = Color.Red;
                    newNode = newNode.Parent.Parent;
                }
                else
                {
                    if (newNode == newNode.Parent.Right)
                    {
                        newNode = newNode.Parent;
                        LeftRotate(newNode);
                    }

                    newNode.Parent.Color = Color.Black;
                    newNode.Parent.Parent.Color = Color.Red;
                    RightRotate(newNode.Parent.Parent);
                }
            }
            else
            {
                Node y = newNode.Parent.Parent.Left;
                if (y.Color == Color.Red)
                {
                    newNode.Parent.Color = Color.Black;
                    y.Color = Color.Black;
                    newNode.Parent.Parent.Color = Color.Red;
                    newNode = newNode.Parent.Parent;
                }
                else
                {
                    if (newNode == newNode.Parent.Left)
                    {
                        newNode = newNode.Parent;
                        RightRotate(newNode);
                    }

                    newNode.Parent.Color = Color.Black;
                    newNode.Parent.Parent.Color = Color.Red;
                    LeftRotate(newNode.Parent.Parent);
                }
            }
        }

        Root.Color = Color.Black;
    }

    private void DeleteFixup(Node nodeToDelete)
    {
        while (nodeToDelete != Root && nodeToDelete.Color == Color.Black)
        {
            if (nodeToDelete == nodeToDelete.Parent.Left)
            {
                Node right = nodeToDelete.Parent.Right;
                if (right.Color == Color.Red)
                {
                    right.Color = Color.Black;
                    nodeToDelete.Parent.Color = Color.Red;
                    LeftRotate(nodeToDelete.Parent);
                    right = nodeToDelete.Parent.Right;
                }

                if (right.Left.Color == Color.Black && right.Right.Color == Color.Black)
                {
                    right.Color = Color.Red;
                    nodeToDelete = nodeToDelete.Parent;
                }
                else
                {
                    if (right.Right.Color == Color.Black)
                    {
                        right.Left.Color = Color.Black;
                        right.Color = Color.Red;
                        RightRotate(right);
                        right = nodeToDelete.Parent.Right;
                    }

                    right.Color = nodeToDelete.Parent.Color;
                    nodeToDelete.Parent.Color = Color.Black;
                    right.Right.Color = Color.Black;
                    LeftRotate(nodeToDelete.Parent);
                    nodeToDelete = Root;
                }
            }
            else
            {
                Node w = nodeToDelete.Parent.Left;
                if (w.Color == Color.Red)
                {
                    w.Color = Color.Black;
                    nodeToDelete.Parent.Color = Color.Red;
                    RightRotate(nodeToDelete.Parent);
                    w = nodeToDelete.Parent.Left;
                }

                if (w.Right.Color == Color.Black && w.Left.Color == Color.Black)
                {
                    w.Color = Color.Red;
                    nodeToDelete = nodeToDelete.Parent;
                }
                else
                {
                    if (w.Left.Color == Color.Black)
                    {
                        w.Right.Color = Color.Black;
                        w.Color = Color.Red;
                        LeftRotate(w);
                        w = nodeToDelete.Parent.Left;
                    }

                    w.Color = nodeToDelete.Parent.Color;
                    nodeToDelete.Parent.Color = Color.Black;
                    w.Left.Color = Color.Black;
                    RightRotate(nodeToDelete.Parent);
                    nodeToDelete = Root;
                }
            }
        }

        nodeToDelete.Color = Color.Black;
    }

    private void Transplant(Node u, Node v)
    {
        if (u.Parent == _nullNode)
            Root = v;
        else if (u == u.Parent.Left)
            u.Parent.Left = v;
        else
            u.Parent.Right = v;

        v.Parent = u.Parent;
    }

    private Node Minimum(Node node)
    {
        while (node.Left != _nullNode) 
            node = node.Left;

        return node;
    }
    
    
    private IEnumerable<KeyValuePair<int, T>> InOrderTraversal(Node node)
    {
        if (node == _nullNode)
            return new List<KeyValuePair<int, T>>();

        var nodes = new List<KeyValuePair<int, T>>();
        nodes.AddRange(InOrderTraversal(node.Left));
        nodes.Add(new KeyValuePair<int, T>(node.Key, node.Value));
        nodes.AddRange(InOrderTraversal(node.Right));

        return nodes;
    }

    private IEnumerable<KeyValuePair<int, T>> PreOrderTraversal(Node node)
    {
        if (node == _nullNode)
            return new List<KeyValuePair<int, T>>();

        var nodes = new List<KeyValuePair<int, T>> { new(node.Key, node.Value) };
        nodes.AddRange(PreOrderTraversal(node.Left));
        nodes.AddRange(PreOrderTraversal(node.Right));

        return nodes;
    }

    private IEnumerable<KeyValuePair<int, T>> PostOrderTraversal(Node node)
    {
        if (node == _nullNode)
            return new List<KeyValuePair<int, T>>();

        List<KeyValuePair<int, T>> nodes = new();
        nodes.AddRange(PostOrderTraversal(node.Left));
        nodes.AddRange(PostOrderTraversal(node.Right));
        nodes.Add(new(node.Key, node.Value));

        return nodes;
    }
}