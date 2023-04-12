using NUnit.Framework;

namespace ATD.Lists.Tests;

public abstract class LinkedListTests
{
    private ILinkedList<int> _intList;
    private ILinkedList<string> _stringList;

    protected void Initialize(ILinkedList<int> intList, ILinkedList<string> stringList)
    {
        _intList = intList;
        _stringList = stringList;
        
        _intList.Insert(0, 1);
        _intList.Insert(1, 2);
        
        _stringList.Insert(0, "Hello,");
        _stringList.Insert(1, "World");
    }
    
    [Test]
    public void InsertToEndTest()
    {
        _intList.Insert(2, 3);

        Assert.AreEqual(1, _intList.GetItem(0));
        Assert.AreEqual(2, _intList.GetItem(1));
        Assert.AreEqual(3, _intList.GetItem(2));
        
        _stringList.Insert(2, "!");

        Assert.AreEqual("Hello,", _stringList.GetItem(0));
        Assert.AreEqual("World", _stringList.GetItem(1));
        Assert.AreEqual("!", _stringList.GetItem(2));
    }
    
    [Test]
    public void InsertWrongPositionTest()
    {
        Assert.Throws<ArgumentException>(() => _intList.Insert(3, 3));
    }
    
    [Test]
    public void InsertToMiddleTest()
    {
        _intList.Insert(2, 3);
        _intList.Insert(1, 5);

        Assert.AreEqual(1, _intList.GetItem(0));
        Assert.AreEqual(5, _intList.GetItem(1));
        Assert.AreEqual(2, _intList.GetItem(2));
        
        _stringList.Insert(2, "!");
        _stringList.Insert(1, " ");

        Assert.AreEqual("Hello,", _stringList.GetItem(0));
        Assert.AreEqual(" ", _stringList.GetItem(1));
        Assert.AreEqual("World", _stringList.GetItem(2));
    }
    
    [Test]
    public void InsertHeadTest()
    {
        _intList.Insert(0, 6);
        Assert.AreEqual(6, _intList.GetItem(0));
        Assert.AreEqual(1, _intList.GetItem(1));
        
        _stringList.Insert(0, "Big ");
        Assert.AreEqual("Big ", _stringList.GetItem(0));
        Assert.AreEqual("Hello,", _stringList.GetItem(1));
    }

    [Test]
    public void DeleteFirstTest()
    {
        _intList.Delete(0);
        Assert.AreEqual(2, _intList.GetItem(0));
        Assert.Throws<ArgumentException>(() => _intList.GetItem(1));
        
        _stringList.Delete(0);
        Assert.AreEqual("World", _stringList.GetItem(0));
        Assert.Throws<ArgumentException>(() => _stringList.GetItem(1));
    }

    [Test]
    public void DeleteFromMiddleTest()
    {
        _intList.Insert(2, 3);
        _intList.Delete(1);
        
        Assert.AreEqual(3, _intList.GetItem(1));
        Assert.Throws<ArgumentException>(() => _intList.GetItem(2));
        
        _stringList.Insert(2, "!");
        _stringList.Delete(1);
        
        Assert.AreEqual("!", _stringList.GetItem(1));
        Assert.Throws<ArgumentException>(() => _stringList.GetItem(2));
    }

    [Test]
    public void DeleteLastTest()
    {
        _intList.Delete(1);
        Assert.Throws<ArgumentException>(() => _intList.GetItem(1));
        
        _stringList.Delete(1);
        Assert.Throws<ArgumentException>(() => _stringList.GetItem(1));
    }
    
    [Test]
    public void DeleteWrongPositionTest()
    {
        Assert.Throws<ArgumentException>(() => _intList.Delete(2));
        Assert.Throws<ArgumentException>(() => _stringList.Delete(2));
    }
    
    [Test]
    public void DeleteAllElementsThenInsertTest()
    {
        _intList.Delete(0);
        _intList.Delete(0);
        
        Assert.Throws<ArgumentException>(() => _intList.Insert(1, 8));
        _intList.Insert(0, 7);
        _intList.Insert(1, 9);
        Assert.AreEqual(7, _intList.GetItem(0));
        Assert.AreEqual(9, _intList.GetItem(1));
        Assert.Throws<ArgumentException>(() => _intList.GetItem(2));
    }
    
    [Test]
    public void LookupTest()
    {
        _intList.Insert(2, 3);
        
        var index1 = _intList.Lookup(1);
        var index2 = _intList.Lookup(2);
        var index3 = _intList.Lookup(3);
        var index4 = _intList.Lookup(4);
        
        Assert.AreEqual(0, index1.Value);
        Assert.AreEqual(1, index2.Value);
        Assert.AreEqual(2, index3.Value);
        Assert.False(index4.HasValue);
    }
    
    
    [Test]
    public void PrevTest()
    {
        _stringList.Insert(1, " ");
        _stringList.Insert(3, "!");

        var prev2 = _stringList.Prev(2);
        var prev3 = _stringList.Prev(3);
        var prev1 = _stringList.Prev(1);
        var prev0 = _stringList.Prev(0);
        
        Assert.AreEqual(1, prev2.Value);
        Assert.AreEqual(2, prev3.Value);
        Assert.AreEqual(0, prev1.Value);
        Assert.False(prev0.HasValue);
    }
    
    [Test]
    public void NextTest()
    {
        _intList.Insert(2, 3);
        _intList.Insert(3, 4);

        var next2 = _intList.Next(2);
        var next3 = _intList.Next(3);
        var next1 = _intList.Next(1);
        var next0 = _intList.Next(0);
        
        Assert.AreEqual(3, next2.Value);
        Assert.False(next3.HasValue);
        Assert.AreEqual(2, next1.Value);
        Assert.AreEqual(1, next0.Value);
    }
}