using ATD.Lists;

namespace ATD.Deque;

public class Deque<T> : IDeque<T>
{
    private class Node
    {
        public T Data { get; private set; }
        public Node Next { get; set; }
        public Node Prev { get; set; }

        public Node(T data, Node next, Node prev)
        {
            Next = next;
            Data = data;
            Prev = prev;
        }
    }

    private Node _head;
    private Node _tail;

    private bool IsEmpty => _head == null || _tail == null;

    public void PushBack(T data)
    {
        var newNode = new Node(data, null, _tail);

        if (!IsEmpty) 
            _tail.Next = newNode;
        else
            _head = newNode;

        _tail = newNode;
    }
    
    public void PushFront(T data)
    {
        var newNode = new Node(data, _head, null);

        if (!IsEmpty) 
            _head.Prev = newNode;
        else
            _tail = newNode;

        _head = newNode;
    }

    public T PopFront()
    {
        var data = Front();
        _head = _head.Next;
        _head.Prev = null;
        return data;
    }

    public T PopBack()
    {
        var data = Back();
        _tail = _tail.Prev;
        _tail.Next = null;
        return data;
    }

    public T Front()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Deque is empty.");
        
        return _head.Data;
    }

    public T Back()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Deque is empty.");
            
        return _tail.Data;
    }
}