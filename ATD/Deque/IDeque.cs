namespace ATD.Deque;

public interface IDeque<T>
{
    void PushBack(T data);
    void PushFront(T data);
    T PopFront();
    T PopBack();
    T Front();
    T Back();
}