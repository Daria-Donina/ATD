namespace ATD.Queue;

public interface IPriorityQueue<T>
{
    void Enqueue(int key, T data);
    T DequeueMin();
    T Front();
}