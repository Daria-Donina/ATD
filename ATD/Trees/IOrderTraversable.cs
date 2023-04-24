namespace ATD.Trees;

public interface IOrderTraversable<T>
{
    IEnumerable<KeyValuePair<int, T>> InOrderTraversal();
    IEnumerable<KeyValuePair<int, T>> PreOrderTraversal();
    IEnumerable<KeyValuePair<int, T>> PostOrderTraversal();
}