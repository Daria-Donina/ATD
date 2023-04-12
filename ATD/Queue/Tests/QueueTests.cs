using NUnit.Framework;

namespace ATD.Queue.Tests;

public class SimpleQueueTests
{
    private IQueue<string> _queue;
    
    [SetUp]
    public void Initialize()
    {
        _queue = new Queue<string>();
    }

    [Test]
    public void QueueSmokeTest()
    {
        _queue.Enqueue("hey");
        _queue.Enqueue("hi");
        
        Assert.AreEqual("hey", _queue.Front());
        Assert.AreEqual("hey", _queue.Dequeue());
        Assert.AreEqual("hi", _queue.Dequeue());
        
        Assert.Throws<InvalidOperationException>(() => _queue.Front());
        Assert.Throws<InvalidOperationException>(() => _queue.Dequeue());
    }
    
    [Test]
    public void QueueEnqueueAfterDequeueTest()
    {
        _queue.Enqueue("good");
        _queue.Enqueue("better");
        
        Assert.AreEqual("good", _queue.Dequeue());
        
        _queue.Enqueue("the best");
        Assert.AreEqual("better", _queue.Front());

        _queue.Dequeue();
        Assert.AreEqual("the best", _queue.Dequeue());
    }
}