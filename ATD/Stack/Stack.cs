namespace ATD.Stack;

public class Stack<T> : IStack<T>
{
    private readonly T[] _items;
    private int _length;

    public Stack(int capacity)
    {
        _items = new T[capacity];
    }

    public void Push(T data)
    {
        if (_length == _items.Length)
            throw new StackOverflowException($"Stack is full. Capacity is {_length + 1} elements.");

        _items[_length] = data;
        _length++;
    }

    public T Pop()
    {
        var element = Top();
        
        _items[_length - 1] = default;
        _length--;

        return element;
    }

    public T Top()
    {
        if (_length == 0)
            throw new InvalidOperationException("Stack is empty.");

        return _items[_length - 1];
    }
}