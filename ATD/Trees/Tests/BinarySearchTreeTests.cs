using NUnit.Framework;

namespace ATD.Trees.Tests;

public class BinarySearchTreeTests : ITreeTests<BinarySearchTree<string>.Node>
{
    [SetUp]
    public void Initialize()
    {
        _tree = new BinarySearchTree<string>();
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