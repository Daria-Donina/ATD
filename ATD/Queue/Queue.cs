using ATD.Lists;

namespace ATD.Queue;

public class Queue<T> : IQueue<T>
{
    private class Node
    {
        public T Data { get; private set; }
        public Node Next { get; set; }

        public Node(T data, Node next)
        {
            Next = next;
            Data = data;
        }
    }

    private Node _head;
    private Node _tail;

    private bool IsEmpty => _head == null || _tail == null;

    public void Enqueue(T data)
    {
        var newNode = new Node(data, null);
        
        if (!IsEmpty) 
            _tail.Next = newNode;
        else
            _head = newNode;

        _tail = newNode;
    }

    public T Dequeue()
    {
        var data = Front();
        _head = _head.Next;
        return data;
    }

    public T Front()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Queue is empty.");

        return _head.Data;
    }
}