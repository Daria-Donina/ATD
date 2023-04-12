namespace ATD.Lists;

public interface ILinkedList<T>
{
    int Length { get; }
    void Insert(int position, T data);
    void Delete(int position);
    T GetItem(int position);
    int? Lookup(T data);
    int? Prev(int position);
    int? Next(int position);
}