namespace ATD.Trees;

public interface ITree<T, out TNode>
{
    public TNode Root { get; }
    void Insert(int key, T value);
    bool Lookup(int key, out T value);
    void Remove(int key);
}