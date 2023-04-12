using NUnit.Framework;

namespace ATD.Lists.Tests;

[TestFixture]
public class DoubleLinkedListTests : LinkedListTests
{
    [SetUp]
    public void Initialize()
    {
        base.Initialize(new DoubleLinkedList<int>(), new DoubleLinkedList<string>());
    }
}