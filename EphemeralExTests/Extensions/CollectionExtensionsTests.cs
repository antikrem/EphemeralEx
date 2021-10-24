﻿using EphemeralEx.Extensions;

using System.Linq;

using NUnit.Framework;
using FluentAssertions;

using EphemeralEx.Tests;


namespace EphemeralExTests.Extensions
{
    public class CollectionExtensionsTests
    {
        [Test]
        public void ForEach_WithPredicate_ExecutePredicateOnSequence()
        {
            var item1 = Some.Int(-1000, 1000);
            var item2 = Some.Int(-1000, 1000);
            var item3 = Some.Int(-1000, 1000);
            var items = new int[] { item1, item2, item3 };

            var result = 0;
            items.ForEach(value => result += value);

            result.Should().Be(item1 + item2 + item3);
        }

        [Test]
        public void ToEnumerable_WithValueItem_ReturnsEnumerable()
        {
            var item = Some.Int();

            var result = item.ToEnumerable();

            result.Should().BeEquivalentTo(new int[] { item }.ToList());
        }

        [Test]
        public void ToEnumerable_WithReferenceItem_ReturnsEnumerable()
        {
            var item = new object();

            var result = item.ToEnumerable();

            result.Should().BeEquivalentTo(new object[] { item }.ToList());
        }

        [Test]
        public void None_WithFalseConditionToAllElements_ReturnsTrue()
        {
            var items = new int[] { Some.Int(0, 1000), Some.Int(0, 1000), Some.Int(0, 1000) };

            var result = items.None(value => value < 0);

            result.Should().BeTrue();
        }

        [Test]
        public void None_WithTrueConditionToAllElements_ReturnsFalse()
        {
            var items = new int[] { Some.Int(0, 1000), Some.Int(0, 1000), Some.Int(0, 1000) };

            var result = items.None(value => value > 0);

            result.Should().BeFalse();
        }

        [Test]
        public void None_WithMixedCondition_ReturnsFalse()
        {
            var items = new int[] { -Some.Int(0, 1000), Some.Int(0, 1000), Some.Int(0, 1000) };

            var result = items.None(value => value < 0);

            result.Should().BeFalse();
        }

        [Test]
        public void ChainCall_WithSequence_CallsActionForEachElement()
        {
            var item1 = Some.Int(0, 1000);
            var item2 = Some.Int(0, 1000);
            var item3 = Some.Int(0, 1000);
            var sequence = new int[] { item1, item2, item3 };
            var start = "Numbers:";

            var result = start.ChainCall(sequence, (str, element) => str + element.ToString());

            result.Should().Be("Numbers:" + item1.ToString() + item2.ToString() + item3.ToString());
        }
    }
}
