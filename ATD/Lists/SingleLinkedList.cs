namespace ATD.Lists;

public class SingleLinkedList<T> : ILinkedList<T>
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

    public int Length { get; private set; }

    public void Insert(int position, T data)
    {
        if (!PositionCorrectForInsert(position))
            throw new ArgumentException("Incorrect position for insert.");

        if (position == 0)
            _head = new Node(data, _head);
        else
        {
            var prevNode = NodeByPosition(position - 1);
            prevNode.Next = new Node(data, prevNode.Next);
        }

        Length++;
    }

    public void Delete(int position)
    {
        if (!PositionInList(position))
            throw new ArgumentException("Incorrect position for delete.");

        if (position == 0)
            _head = _head.Next;
        else
        {
            var prevNode = NodeByPosition(position - 1);
            prevNode.Next = prevNode.Next.Next;
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
        var current = _head;
        var position = 0;
        
        while (current != null && !data.Equals(current.Data))
        {
            current = current.Next;
            position++;
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