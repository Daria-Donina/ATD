using NUnit.Framework;

namespace ATD.Lists.Tests;

[TestFixture]
public class SingleLinkedListTests : LinkedListTests
{
    [SetUp]
    public void Initialize()
    {
        base.Initialize(new SingleLinkedList<int>(), new SingleLinkedList<string>());
    }
}