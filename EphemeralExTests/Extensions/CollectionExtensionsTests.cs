using EphemeralEx.Extensions;

using System.Linq;

using NUnit.Framework;
using FluentAssertions;

using EphemeralEx.Tests;
using System.Collections.Generic;
using System;

namespace EphemeralExTests.Extensions
{
    public class CollectionExtensionsTests
    {
        [Test]
        public void ForEach_WithPredicate_ExecutePredicateOnSequence()
        {
            var item1 = Dummy.Int(-1000, 1000);
            var item2 = Dummy.Int(-1000, 1000);
            var item3 = Dummy.Int(-1000, 1000);
            var items = new int[] { item1, item2, item3 };

            var result = 0;
            items.ForEach(value => result += value);

            result.Should().Be(item1 + item2 + item3);
        }

        [Test]
        public void ToEnumerable_WithValueItem_ReturnsEnumerable()
        {
            var item = Dummy.Int();

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
            var items = new int[] { Dummy.Int(0, 1000), Dummy.Int(0, 1000), Dummy.Int(0, 1000) };

            var result = items.None(value => value < 0);

            result.Should().BeTrue();
        }

        [Test]
        public void None_WithTrueConditionToAllElements_ReturnsFalse()
        {
            var items = new int[] { Dummy.Int(0, 1000), Dummy.Int(0, 1000), Dummy.Int(0, 1000) };

            var result = items.None(value => value > 0);

            result.Should().BeFalse();
        }

        [Test]
        public void None_WithMixedCondition_ReturnsFalse()
        {
            var items = new int[] { -Dummy.Int(0, 1000), Dummy.Int(0, 1000), Dummy.Int(0, 1000) };

            var result = items.None(value => value < 0);

            result.Should().BeFalse();
        }

        [Test]
        public void ChainCall_WithSequence_CallsActionForEachElement()
        {
            var item1 = Dummy.Int(0, 1000);
            var item2 = Dummy.Int(0, 1000);
            var item3 = Dummy.Int(0, 1000);
            var sequence = new int[] { item1, item2, item3 };
            var start = "Numbers:";

            var result = start.ChainCall(sequence, (str, element) => str + element.ToString());

            result.Should().Be("Numbers:" + item1.ToString() + item2.ToString() + item3.ToString());
        }

        [Test]
        public void AddAndReturn_WithNewEntry_AddsToDictionary()
        {
            var dictionary = new Dictionary<int, string>() { {1, "foo"} };

            var result = dictionary.AddAndReturn(2, "bar");

            result.Should().Be("bar");
            dictionary.Should().HaveCount(2);
            dictionary.Should().BeEquivalentTo(
                    new Dictionary<int, string>()
                    {
                        { 1, "foo" },
                        { 2, "bar" }
                    }
                );
        }

        [Test]
        public void AddAndReturn_WithExistingEntry_ShouldThrow()
        {
            var dictionary = new Dictionary<int, string>() { { 1, "foo" } };

            dictionary.Invoking(d => d.AddAndReturn(1, "bar"))
                .Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetOrAdd_WithNewEntry_CreatesNewEntryAndReturns()
        {
            var dictionary = new Dictionary<int, string>() { { 1, "foo" } };

            var result = dictionary.GetOrAdd(2, "bar");

            result.Should().Be("bar");
            dictionary.Should().HaveCount(2);
            dictionary.Should().BeEquivalentTo(
                    new Dictionary<int, string>()
                    {
                        { 1, "foo" },
                        { 2, "bar" }
                    }
                );
        }

        [Test]
        public void GetOrAdd_WithExistingEntry_ReturnsWithoutCreating()
        {
            var dictionary = new Dictionary<int, string>() { { 1, "foo" } };

            var result = dictionary.GetOrAdd(1, "bar");

            result.Should().Be("foo");
            dictionary.Should().HaveCount(1);
            dictionary.Should().BeEquivalentTo(
                    new Dictionary<int, string>()
                    {
                        { 1, "foo" }
                    }
                );
        }

        [Test]
        public void GetOrAddOverload_WithNewEntry_CreatesNewEntryAndReturns()
        {
            var dictionary = new Dictionary<int, string>() { { 1, "foo" } };

            var result = dictionary.GetOrAdd(2, () => "bar");

            result.Should().Be("bar");
            dictionary.Should().HaveCount(2);
            dictionary.Should().BeEquivalentTo(
                    new Dictionary<int, string>()
                    {
                        { 1, "foo" },
                        { 2, "bar" }
                    }
                );
        }

        [Test]
        public void GetOrAddOverload_WithExistingEntry_ReturnsWithoutCreating()
        {
            var dictionary = new Dictionary<int, string>() { { 1, "foo" } };

            var result = dictionary.GetOrAdd(1, () => "bar");

            result.Should().Be("foo");
            dictionary.Should().HaveCount(1);
            dictionary.Should().BeEquivalentTo(
                    new Dictionary<int, string>()
                    {
                        { 1, "foo" }
                    }
                );
        }
    }
}
