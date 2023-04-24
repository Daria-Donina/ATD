namespace ATD.Trees;

public class Trie : ITrie
{
    public class Node
    {
        public Dictionary<char, Node> Children { get; }
        public bool IsCompleteWord { get; set; }

        public Node()
        {
            Children = new Dictionary<char, Node>();
        }
    }

    public Trie()
    {
        Root = new Node();
    }

    private Node Root { get; }

    public void Insert(string word)
    {
        Node current = Root;
        char[] characters = word.ToLower().ToCharArray();
        foreach (char c in characters)
        {
            Node child = current.Children.GetValueOrDefault(c);
            if (child is null)
            {
                child = new Node();
                current.Children.Add(c, child);
            }
            current = child;
        }
        current.IsCompleteWord = true;
    }

    public bool Search(string word)
    {
        Node node = GetNode(word.ToLower());
        return GetNode(word) is not null && node.IsCompleteWord;
    }

    public bool StartsWith(string prefix)
    {
        return GetNode(prefix.ToLower()) is not null;
    }

    private Node GetNode(string word)
    {
        Node current = Root;
        char[] characteres = word.ToLower().ToCharArray();
        foreach (var c in characteres)
        {
            Node child = current.Children.GetValueOrDefault(c);
            if (child is null) return null;
            current = child;
        }
        return current;
    }
}