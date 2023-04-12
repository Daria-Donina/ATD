using ATD.Lists;

namespace ATD.Queue;

public class PriorityQueue<T> : IPriorityQueue<T>
{
    private class Node
    {
        public int Priority { get; private set; }
        public T Value { get; private set; }
        public Node Next { get; set; }

        public Node(int priority, T value, Node next)
        {
            Priority = priority;
            Value = value;
            Next = next;
        }
    }
    
    private Node _head;
    
    public void Enqueue(int key, T data)
    {
        var current = _head;

        if (current == null || current.Priority > key)
        {
            _head = new Node(key, data, _head);
            return;
        }
        
        while (current != null)
        {
            if (current.Next == null || current.Priority <= key && current.Next.Priority > key)
            {
                InsertAfter(current, key, data);
                return;
            }

            current = current.Next;
        }
    }

    public T DequeueMin()
    {
        var data = Front();
        _head = _head.Next;
        return data;
    }

    public T Front()
    {
        if (_head == null)
            throw new InvalidOperationException("Queue is empty.");
        
        return _head.Value;
    }

    private void InsertAfter(Node prev, int key, T data) => 
        prev.Next = new Node(key, data, prev.Next);
}