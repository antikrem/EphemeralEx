using EphemeralEx.Extensions;

using NUnit.Framework;
using FluentAssertions;

using System;
using System.Linq;
using System.Collections.Generic;

using EphemeralEx.Tests;


namespace EphemeralExTests.Extensions
{
    class EnumerableExtensionsTests
    {
        [Test]
        public void OfType_WithMixedElements_FiltersOnlyRequestedType()
        {
            var item1 = Dummy.Int();
            var item2 = Dummy.String();
            var item3 = Dummy.Int();
            var items = new object[] { item1, item2, item3 };

            var result = items.OfType<string>();

            result.Should().HaveCount(1);
            result.Should().BeEquivalentTo(item2.ToEnumerable());
        }

        [Test]
        public void IndexBy_WithCorrectInput_CreatesExpectedDictionary()
        {
            var sequence = new List<(int Index, string Text)>
            {
                (1, "Foo"),
                (3, "Fizz"),
                (2, "Bar")
            };

            var result = sequence.IndexBy(item => item.Index);

            result.Should().BeEquivalentTo(
                new Dictionary<int, (int, string)>
                {
                    { 1, (1, "Foo") },
                    { 3, (3, "Fizz") },
                    { 2, (2, "Bar") }
                }
            );
        }

        [Test]
        public void IndexBy_WithDuplicateIndex_ThrowsArgumentException()
        {
            var sequence = new List<(int Index, string Text)>
            {
                (1, "Foo"),
                (3, "Bar"),
                (3, "Bar2")
            };

            sequence.Invoking(sut => sut.IndexBy(item => item.Index)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void NotNull_WithMixedElements_FiltersAllNulls()
        {
            var item1 = Dummy.String();
            var item2 = (string?)null;
            var item3 = Dummy.String();
            var items = new string?[] { item1, item2, item3 };

            var result = items.NotNull();

            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(new string[] { item1, item3 });
        }

        [Test]
        public void Flatten_WithSequenceOfSequences_ReturnsOneSequence()
        {
            var item1 = Dummy.Int();
            var item2 = Dummy.Int();
            var item3 = Dummy.Int();
            var item4 = Dummy.Int();
            var items = new int[][] { new int[] { item1, item2 }, new int[] { item3, item4 } };

            var result = items.Flatten();

            result.Should().BeEquivalentTo(new int[] { item1, item2, item3, item4 });
        }

        [Test]
        public void DistinctBy_WithMixedElements_RemovesFollowingDuplicates()
        {
            var items = new (int Index, string Value)[] { (1, "a"), (2, "a"), (3, "a"), (1, "b") };

            var result = items.DistinctBy(join => join.Index);

            result.Should().BeEquivalentTo(
                    new (int Index, string Value)[] { (1, "a"), (2, "a"), (3, "a") }
                );
        }

        [Test]
        public void SelectSuccessful_WithCorrectUsage_OnlyTakesSuccessfulElements()
        {
            var items = new int[] { 1, 2, 0, 3 };

            var result = items.SelectSuccessful<DivideByZeroException, int, int>(value => 6 / value);

            result.Should().BeEquivalentTo(new List<int> { 6, 3, 2 });
        }

        [Test]
        public void SelectSuccessful_WithIncorrectExceptionArgument_OnlyTakesSuccessfulElements()
        {
            var items = new int[] { 1, 2, 0, 3 };

            items.Invoking(i => i.SelectSuccessful<ArgumentException, int, int>(value => 6 / value))
                .Should().Throw<DivideByZeroException>();
        }

        [Test]
        public void SelectSuccessful_WithExceptionHandler_RunsExceptionHandler()
        {
            var items = new int[] { 1, 2, 0, 3 };
            var handled = false;

            var result = items.SelectSuccessful<DivideByZeroException, int, int>(value => 6 / value, (_, _) => handled = true);

            handled.Should().BeTrue();
        }

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
    }
}
