using EphemeralEx.Extensions;

using FluentAssertions;
using NUnit.Framework;

using System.Collections.Generic;
using System.Linq;


namespace EphemeralExTests.Extensions
{
    class TreeExtensionsTests
    {
        [Test]
        public void TraverseTree_WithTreeStructure_TraverseTree()
        {
            var tree = new Node(
                0,
                new List<Node>() {
                    new Node(1, Enumerable.Empty<Node>().ToList()),
                    new Node(
                        2,
                        new List<Node>() {
                            new Node(3, Enumerable.Empty<Node>().ToList()),
                            new Node(4, Enumerable.Empty<Node>().ToList())
                        }
                    )
                }
            );

            var results = tree.TraverseTree(node => node.Nodes);

            results
                .Select(node => node.Value)
                .Should()
                .BeEquivalentTo(new List<int> { 0, 1, 2, 3, 4 });
        }

        private record Node(int Value, List<Node> Nodes);
    }
}
