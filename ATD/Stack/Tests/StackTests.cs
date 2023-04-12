using NUnit.Framework;

namespace ATD.Stack.Tests;

public class StackTests
{
    private IStack<int> _intStack;
    private IStack<string> _stringStack;

    [SetUp]
    public void Initialize()
    {
        _intStack = new Stack<int>(1000);
        _stringStack = new Stack<string>(1000);
    }

    [Test]
    public void IntStackTest()
    {
        _intStack.Push(5);
        _intStack.Push(4);
        
        Assert.AreEqual(4, _intStack.Top());
        Assert.AreEqual(4, _intStack.Pop());
        Assert.AreEqual(5, _intStack.Pop());
        
        Assert.Throws<InvalidOperationException>(() => _intStack.Top());
        Assert.Throws<InvalidOperationException>(() => _intStack.Pop());
    }
    
    [Test]
    public void StringStackTest()
    {
        _stringStack.Push("hey");
        _stringStack.Push("hi");
        
        Assert.AreEqual("hi", _stringStack.Top());
        Assert.AreEqual("hi", _stringStack.Pop());
        Assert.AreEqual("hey", _stringStack.Pop());
        
        Assert.Throws<InvalidOperationException>(() => _stringStack.Top());
        Assert.Throws<InvalidOperationException>(() => _stringStack.Pop());
    }
    
    [Test]
    public void StackOverflowPushTest()
    {
        var shortStack = new Stack<int>(2);
        
        shortStack.Push(1);
        shortStack.Push(2);
        
        Assert.AreEqual(2, shortStack.Top());
        Assert.Throws<StackOverflowException>(() => shortStack.Push(3));
    }
    
    [Test]
    public void PushAfterPopTest()
    {
        _stringStack.Push("good");
        _stringStack.Push("better");
        
        Assert.AreEqual("better", _stringStack.Pop());
        
        _stringStack.Push("the best");
        Assert.AreEqual("the best", _stringStack.Top());

        _stringStack.Pop();
        Assert.AreEqual("good", _stringStack.Pop());
    }
}