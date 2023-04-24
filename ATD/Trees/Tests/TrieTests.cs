using NUnit.Framework;

namespace ATD.Trees.Tests;

public class TrieTests
{
    private readonly string[] _words = { "mouse", "screen", "board", "hat", "car", "card" };
    private ITrie _trie;

    [SetUp]
    public void Initialize()
    {
        _trie = new Trie();
    }

    [Test]
    public void InsertWords()
    {
        bool allWordsInserted = true;
        _words.ToList().ForEach(word => _trie.Insert(word));
        _words.ToList().ForEach(word =>
        {
            if (!_trie.Search(word))
                allWordsInserted = false;
        });
        Assert.IsTrue(allWordsInserted);
    }

    [Test]
    public void SearchForWordThatExists()
    {
        _words.ToList().ForEach(word => _trie.Insert(word));
        bool result = _trie.Search("mouse");
        Assert.IsTrue(result);
    }

    [Test]
    public void SearchForWordThatNotExists()
    {
        _words.ToList().ForEach(word => _trie.Insert(word));
        bool result = _trie.Search("book");
        Assert.IsFalse(result);
    }

    [Test]
    public void SearchForPrefixThatExists()
    {
        _words.ToList().ForEach(word => _trie.Insert(word));
        bool result = _trie.StartsWith("mo");
        Assert.IsTrue(result);
    }

    [Test]
    public void SearchForPrefixThatNotExists()
    {
        _words.ToList().ForEach(word => _trie.Insert(word));
        bool result = _trie.Search("tra");
        Assert.IsFalse(result);
    }
}