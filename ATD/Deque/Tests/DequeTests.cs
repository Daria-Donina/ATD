using NUnit.Framework;

namespace ATD.Deque.Tests;

public class DequeTests
{
    private IDeque<string> _deque; 

    [SetUp]
    public void Initialize()
    {
        _deque = new Deque<string>();
    }

    [Test]
    public void PushFrontTest()
    {
        _deque.PushFront("!");
        _deque.PushFront("world");
        _deque.PushFront("hello, ");

        Assert.AreEqual("hello, ", _deque.Front());
        Assert.AreEqual("!", _deque.Back());
        _deque.PopBack();
        
        Assert.AreEqual("world", _deque.Back());
    }
    
    [Test]
    public void PushBackTest()
    {
        _deque.PushBack("hello, ");
        _deque.PushBack("world");
        _deque.PushBack("!");

        Assert.AreEqual("hello, ", _deque.Front());
        Assert.AreEqual("!", _deque.Back());
        _deque.PopFront();
        
        Assert.AreEqual("world", _deque.Front());
    }
    
    [Test]
    public void AllPushTest()
    {
        _deque.PushBack("world");
        _deque.PushFront("dear ");
        _deque.PushFront("hello, ");
        _deque.PushBack("!");

        Assert.AreEqual("hello, ", _deque.Front());
        Assert.AreEqual("!", _deque.Back());
        _deque.PopFront();
        Assert.AreEqual("dear ", _deque.PopFront());
        Assert.AreEqual("world", _deque.PopFront());
    }

    [Test]
    public void PopEmptyTest()
    {
        Assert.Throws<InvalidOperationException>(() => _deque.PopBack());
        Assert.Throws<InvalidOperationException>(() => _deque.PopFront());
    }
}