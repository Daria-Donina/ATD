namespace ATD.Lists;

public class DoubleLinkedList<T> : ILinkedList<T>
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

    public int Length { get; private set; }

    public void Insert(int position, T data)
    {
        if (!PositionCorrectForInsert(position))
            throw new ArgumentException("Incorrect position for insert.");

        if (position == 0)
        {
            var newNode = new Node(data, _head, null);
            if (_head != null)
                _head.Prev = newNode;
            _head = newNode;
            _tail ??= _head;
        }
        else if (position == Length)
        {
            var newNode = new Node(data, null, _tail);
            _tail.Next = newNode;
            _tail = newNode;
        }
        else
        {
            var prevNode = NodeByPosition(position - 1);
            var nextNode = prevNode.Next;
            var newNode = new Node(data, nextNode, prevNode);
            prevNode.Next = newNode;
            nextNode.Prev = newNode;
        }

        Length++;
    }

    public void Delete(int position)
    {
        if (!PositionInList(position))
            throw new ArgumentException("Incorrect position for delete.");

        if (Length == 1)
        {
            _head = null;
            _tail = null;
        }
        else if (position == 0)
        {
            _head = _head.Next;
            _head.Prev = null;
        }
        else if (position == Length - 1)
        {
            _tail = _tail.Prev;
            _tail.Next = null;
        }
        else
        {
            var prevNode = NodeByPosition(position - 1);
            var nextNode = prevNode.Next.Next;
            prevNode.Next = nextNode;
            nextNode.Prev = prevNode;
        }

        Length--;
    }

    public T GetItem(int position)
    {
        if (!PositionInList(position))
            throw new ArgumentException("Incorrect position.");

        return NodeByPosition(position).Data;
    }

    public int? Lookup(T data)
    {
        var current = _tail;
        var position = Length - 1;
        
        while (current != null && !data.Equals(current.Data))
        {
            current = current.Prev;
            position--;
        }

        if (current == null)
            return null;

        return position;
    }

    public int? Prev(int position)
    {
        if (!PositionInList(position - 1))
            return null;

        return position - 1;
    }

    public int? Next(int position)
    {
        if (!PositionInList(position + 1))
            return null;

        return position + 1;
    }
    
    private bool PositionInList(int position) => position >= 0 && position < Length;
    
    private bool PositionCorrectForInsert(int position) => PositionInList(position) || position == Length;
    
    private Node NodeByPosition(int position)
    {
        var current = _head;
        var counter = 0;

        while (current != null && counter < position)
        {
            current = current.Next;
            counter++;
        }

        return current;
    }
}