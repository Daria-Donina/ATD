using NUnit.Framework;

namespace ATD.Queue.Tests;

public class PriorityQueueTests
{
    private IPriorityQueue<int> _priorityQueue;

    [SetUp]
    public void Initialize()
    {
        _priorityQueue = new PriorityQueue<int>();
    }

    [Test]
    public void EnqueueAscendingPrioritiesTest()
    {
        _priorityQueue.Enqueue(0, 234);
        _priorityQueue.Enqueue(1, 235);
        _priorityQueue.Enqueue(2, 236);
        
        Assert.AreEqual(234, _priorityQueue.Front());
        Assert.AreEqual(234, _priorityQueue.DequeueMin());
        Assert.AreEqual(235, _priorityQueue.DequeueMin());
        Assert.AreEqual(236, _priorityQueue.DequeueMin());
        
        Assert.Throws<InvalidOperationException>(() => _priorityQueue.Front());
        Assert.Throws<InvalidOperationException>(() => _priorityQueue.DequeueMin());
    }
    
    [Test]
    public void EnqueueDescendingPrioritiesTest()
    {
        _priorityQueue.Enqueue(10, 234);
        _priorityQueue.Enqueue(9, 235);
        _priorityQueue.Enqueue(8, 236);
        
        Assert.AreEqual(236, _priorityQueue.Front());
        Assert.AreEqual(236, _priorityQueue.DequeueMin());
        Assert.AreEqual(235, _priorityQueue.DequeueMin());
        Assert.AreEqual(234, _priorityQueue.DequeueMin());
        
        Assert.Throws<InvalidOperationException>(() => _priorityQueue.Front());
        Assert.Throws<InvalidOperationException>(() => _priorityQueue.DequeueMin());
    }
    
    [Test]
    public void EnqueueRandomPrioritiesTest()
    {
        _priorityQueue.Enqueue(5, 234);
        _priorityQueue.Enqueue(-2, 235);
        _priorityQueue.Enqueue(10, 236);
        _priorityQueue.Enqueue(4, 237);
        
        Assert.AreEqual(235, _priorityQueue.DequeueMin());
        Assert.AreEqual(237, _priorityQueue.DequeueMin());
        Assert.AreEqual(234, _priorityQueue.DequeueMin());
        Assert.AreEqual(236, _priorityQueue.DequeueMin());
        
        Assert.Throws<InvalidOperationException>(() => _priorityQueue.Front());
        Assert.Throws<InvalidOperationException>(() => _priorityQueue.DequeueMin());
    }
    
    [Test]
    public void EnqueueAfterDequeueTest()
    {
        _priorityQueue.Enqueue(6, 333);
        _priorityQueue.Enqueue(5, 334);
        
        Assert.AreEqual(334, _priorityQueue.DequeueMin());
        
        _priorityQueue.Enqueue(7, 335);
        Assert.AreEqual(333, _priorityQueue.Front());

        _priorityQueue.DequeueMin();
        Assert.AreEqual(335, _priorityQueue.DequeueMin());
    }
}