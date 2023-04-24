using System.Drawing;
using NUnit.Framework;

namespace ATD.Trees.Tests;

public class RedBlackTreeTests : ITreeTests<RedBlackTree<string>.Node>
{
    [SetUp]
    public void Initialize()
    {
        _tree = new RedBlackTree<string>();
    }

    [Test]
    public void TestInsertSingleNode()
    {
        _tree.Insert(1, "One");
        Assert.AreEqual(_tree.Root.Color, Color.Black);
        Assert.AreEqual(_tree.Root.Key, 1);
        Assert.AreEqual(_tree.Root.Value, "One");
    }

    [Test]
    public void TestInsertMultipleNodes()
    {
        _tree.Insert(1, "One");
        _tree.Insert(2, "Two");
        _tree.Insert(3, "Three");
        Assert.IsTrue(_tree.Root.Color == Color.Black);
        Assert.IsTrue(_tree.Root.Key == 2);
        Assert.IsTrue(_tree.Root.Left.Key == 1);
        Assert.IsTrue(_tree.Root.Right.Key == 3);
    }

    [Test]
    public void TestInsertAndRemoveSingleNode()
    {
        _tree.Insert(1, "One");
        _tree.Remove(1);
        Assert.IsNull(_tree.Root.Value);
    }

    [Test]
    public void TestInsertAndRemoveMultipleNodes()
    {
        _tree.Insert(1, "One");
        _tree.Insert(2, "Two");
        _tree.Insert(3, "Three");
        _tree.Insert(4, "Four");
        _tree.Insert(5, "Five");
        _tree.Remove(1);
        _tree.Remove(3);
        _tree.Remove(5);
        Assert.IsTrue(_tree.Root.Key == 4);
        Assert.IsTrue(_tree.Root.Left.Key == 2);
    }

    [Test]
    public void TestSearchForExistingNode()
    {
        _tree.Insert(1, "One");
        _tree.Insert(2, "Two");
        _tree.Insert(3, "Three");
        _tree.Lookup(2, out var value);
        Assert.AreEqual(value, "Two");
    }

    [Test]
    public void TestSearchForNonexistentNode()
    {
        _tree.Insert(1, "One");
        _tree.Insert(2, "Two");
        _tree.Insert(3, "Three");
        Assert.IsFalse(_tree.Lookup(4, out _));
    }

    [Test]
    public void TestInOrderTraversal()
    {
        _tree.Insert(4, "Four");
        _tree.Insert(2, "Two");
        _tree.Insert(1, "One");
        _tree.Insert(3, "Three");
        _tree.Insert(6, "Six");
        _tree.Insert(5, "Five");
        _tree.Insert(7, "Seven");
        List<KeyValuePair<int, string>> expected = new List<KeyValuePair<int, string>>()
        {
            new KeyValuePair<int, string>(1, "One"),
            new KeyValuePair<int, string>(2, "Two"),
            new KeyValuePair<int, string>(3, "Three"),
            new KeyValuePair<int, string>(4, "Four"),
            new KeyValuePair<int, string>(5, "Five"),
            new KeyValuePair<int, string>(6, "Six"),
            new KeyValuePair<int, string>(7, "Seven"),
        };

        var tree = _tree as IOrderTraversable<string>;
        List<KeyValuePair<int, string>> actual = tree.InOrderTraversal().ToList();
        CollectionAssert.AreEqual(expected, actual);
    }
    
    [Test]
    public void Traverse_InOrder_ShouldReturnNodesInOrder()
    {
        _tree.Insert(5, "foo");
        _tree.Insert(3, "bar");
        _tree.Insert(7, "baz");
        _tree.Insert(1, "qux");
        _tree.Insert(4, "quux");
        _tree.Insert(6, "corge");
        _tree.Insert(8, "grault");

        var tree = _tree as IOrderTraversable<string>;
        var nodes = tree.InOrderTraversal().ToList();

        Assert.AreEqual(7, nodes.Count);
        Assert.AreEqual(1, nodes[0].Key);
        Assert.AreEqual("qux", nodes[0].Value);
        Assert.AreEqual(3, nodes[1].Key);
        Assert.AreEqual("bar", nodes[1].Value);
        Assert.AreEqual(4, nodes[2].Key);
        Assert.AreEqual("quux", nodes[2].Value);
        Assert.AreEqual(5, nodes[3].Key);
        Assert.AreEqual("foo", nodes[3].Value);
        Assert.AreEqual(6, nodes[4].Key);
        Assert.AreEqual("corge", nodes[4].Value);
        Assert.AreEqual(7, nodes[5].Key);
        Assert.AreEqual("baz", nodes[5].Value);
        Assert.AreEqual(8, nodes[6].Key);
        Assert.AreEqual("grault", nodes[6].Value);
    }

    [Test]
    public void Traverse_PreOrder_ShouldReturnNodesInPreOrder()
    {
        _tree.Insert(5, "foo");
        _tree.Insert(3, "bar");
        _tree.Insert(7, "baz");
        _tree.Insert(1, "qux");
        _tree.Insert(4, "quux");
        _tree.Insert(6, "corge");
        _tree.Insert(8, "grault");

        var tree = _tree as IOrderTraversable<string>;
        var nodes = tree.PreOrderTraversal().ToList();

        Assert.AreEqual(7, nodes.Count);
        Assert.AreEqual(5, nodes[0].Key);
        Assert.AreEqual("foo", nodes[0].Value);
        Assert.AreEqual(3, nodes[1].Key);
        Assert.AreEqual("bar", nodes[1].Value);
        Assert.AreEqual(1, nodes[2].Key);
        Assert.AreEqual("qux", nodes[2].Value);
        Assert.AreEqual(4, nodes[3].Key);
        Assert.AreEqual("quux", nodes[3].Value);
        Assert.AreEqual(7, nodes[4].Key);
        Assert.AreEqual("baz", nodes[4].Value);
        Assert.AreEqual(6, nodes[5].Key);
        Assert.AreEqual("corge", nodes[5].Value);
        Assert.AreEqual(8, nodes[6].Key);
        Assert.AreEqual("grault", nodes[6].Value);
    }

    [Test]
    public void Traverse_PostOrder_ShouldReturnNodesInPostOrder()
    {
        _tree.Insert(5, "foo");
        _tree.Insert(3, "bar");
        _tree.Insert(7, "baz");
        _tree.Insert(1, "qux");
        _tree.Insert(4, "quux");
        _tree.Insert(6, "corge");
        _tree.Insert(8, "grault");

        var tree = _tree as IOrderTraversable<string>;
        var nodes = tree.PostOrderTraversal().ToList();

        Assert.AreEqual(7, nodes.Count);
        Assert.AreEqual(1, nodes[0].Key);
        Assert.AreEqual("qux", nodes[0].Value);
        Assert.AreEqual(4, nodes[1].Key);
        Assert.AreEqual("quux", nodes[1].Value);
        Assert.AreEqual(3, nodes[2].Key);
        Assert.AreEqual("bar", nodes[2].Value);
        Assert.AreEqual(6, nodes[3].Key);
        Assert.AreEqual("corge", nodes[3].Value);
        Assert.AreEqual(8, nodes[4].Key);
        Assert.AreEqual("grault", nodes[4].Value);
        Assert.AreEqual(7, nodes[5].Key);
        Assert.AreEqual("baz", nodes[5].Value);
        Assert.AreEqual(5, nodes[6].Key);
        Assert.AreEqual("foo", nodes[6].Value);
    }
}