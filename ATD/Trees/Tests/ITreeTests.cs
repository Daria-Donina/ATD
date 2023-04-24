using NUnit.Framework;

namespace ATD.Trees.Tests;

public abstract class ITreeTests<TNode>
{
    protected ITree<string, TNode> _tree;

    [Test]
    public void Insert_SingleElement_ShouldInsertAndFind()
    {
        _tree.Insert(5, "foo");

        bool found = _tree.Lookup(5, out var value);
        Assert.IsTrue(found);
        Assert.AreEqual("foo", value);
    }

    [Test]
    public void Insert_DuplicateKey_ShouldUpdateValue()
    {
        _tree.Insert(5, "foo");
        _tree.Insert(5, "bar");

        bool found = _tree.Lookup(5, out var value);
        Assert.IsTrue(found);
        Assert.AreEqual("bar", value);
    }

    [Test]
    public void Insert_MultipleElements_ShouldInsertAndFind()
    {
        _tree.Insert(5, "foo");
        _tree.Insert(3, "bar");
        _tree.Insert(7, "baz");
        _tree.Insert(1, "qux");
        _tree.Insert(4, "quux");
        _tree.Insert(6, "corge");
        _tree.Insert(8, "grault");

        bool found = _tree.Lookup(5, out var value);
        Assert.IsTrue(found);
        Assert.AreEqual("foo", value);

        found = _tree.Lookup(3, out value);
        Assert.IsTrue(found);
        Assert.AreEqual("bar", value);

        found = _tree.Lookup(7, out value);
        Assert.IsTrue(found);
        Assert.AreEqual("baz", value);

        found = _tree.Lookup(1, out value);
        Assert.IsTrue(found);
        Assert.AreEqual("qux", value);

        found = _tree.Lookup(4, out value);
        Assert.IsTrue(found);
        Assert.AreEqual("quux", value);

        found = _tree.Lookup(6, out value);
        Assert.IsTrue(found);
        Assert.AreEqual("corge", value);

        found = _tree.Lookup(8, out value);
        Assert.IsTrue(found);
        Assert.AreEqual("grault", value);
    }

    [Test]
    public void Delete_RootNode_ShouldDeleteAndNotFind()
    {
        _tree.Insert(5, "foo");

        _tree.Remove(5);

        bool found = _tree.Lookup(5, out _);
        Assert.IsFalse(found);
    }

    [Test]
    public void Delete_LeafNode_ShouldDeleteAndNotFind()
    {
        _tree.Insert(5, "foo");
        _tree.Insert(3, "bar");

        _tree.Remove(3);

        bool found = _tree.Lookup(3, out _);
        Assert.IsFalse(found);
    }

    [Test]
    public void Delete_NodeWithOneChild_ShouldDeleteAndReplaceWithChild()
    {
        _tree.Insert(5, "foo");
        _tree.Insert(3, "bar");
        _tree.Insert(7, "baz");

        _tree.Remove(3);

        bool found = _tree.Lookup(3, out var value);
        Assert.IsFalse(found);

        found = _tree.Lookup(5, out value);
        Assert.IsTrue(found);
        Assert.AreEqual("foo", value);

        found = _tree.Lookup(7, out value);
        Assert.IsTrue(found);
        Assert.AreEqual("baz", value);
    }

    [Test]
    public void Delete_NodeWithTwoChildren_ShouldDeleteAndReplaceWithSuccessor()
    {
        _tree.Insert(5, "foo");
        _tree.Insert(3, "bar");
        _tree.Insert(7, "baz");
        _tree.Insert(1, "qux");
        _tree.Insert(4, "quux");
        _tree.Insert(6, "corge");
        _tree.Insert(8, "grault");

        _tree.Remove(5);

        bool found = _tree.Lookup(5, out var value);
        Assert.IsFalse(found);

        found = _tree.Lookup(3, out value);
        Assert.IsTrue(found);
        Assert.AreEqual("bar", value);

        found = _tree.Lookup(7, out value);
        Assert.IsTrue(found);
        Assert.AreEqual("baz", value);

        found = _tree.Lookup(1, out value);
        Assert.IsTrue(found);
        Assert.AreEqual("qux", value);

        found = _tree.Lookup(4, out value);
        Assert.IsTrue(found);
        Assert.AreEqual("quux", value);

        found = _tree.Lookup(6, out value);
        Assert.IsTrue(found);
        Assert.AreEqual("corge", value);

        found = _tree.Lookup(8, out value);
        Assert.IsTrue(found);
        Assert.AreEqual("grault", value);
    }
    
}