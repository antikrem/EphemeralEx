using EphemeralEx.Extensions;

using NUnit.Framework;
using FluentAssertions;

using System.Linq;
using System.Collections.Generic;

using EphemeralEx.Tests;


namespace EphemeralExTests.Extensions
{
    class SequenceTests
    {
        [Test]
        public void Sequence_From_ComposesEnumerables()
        {
            var sequence1 = new List<int> { Dummy.Int(), Dummy.Int() };
            var sequence2 = new List<int> { Dummy.Int(), Dummy.Int(), Dummy.Int() };
            var sequence3 = new List<int> { Dummy.Int(), Dummy.Int(), Dummy.Int(), Dummy.Int() };

            var result = Sequence.From(sequence1, sequence2, sequence3);

            result.Should().HaveCount(9);
            result.Should().BeEquivalentTo(sequence1.Concat(sequence2).Concat(sequence3));
        }
    }
}
