using ATD.Lists;

namespace ATD.Queue;

public class Queue<T> : IQueue<T>
{
    private readonly ILinkedList<T> _items = new SingleLinkedList<T>();

    public void Enqueue(T data) => 
        _items.Insert(_items.Length, data);

    public T Dequeue()
    {
        var element = Front();
        _items.Delete(0);
        return element;
    }

    public T Front()
    {
        if (_items.Length == 0)
            throw new InvalidOperationException("Queue is empty.");

        return _items.GetItem(0);
    }
}