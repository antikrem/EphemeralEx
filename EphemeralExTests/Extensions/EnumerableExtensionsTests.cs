﻿using EphemeralEx.Extensions;

using System.Linq;

using NUnit.Framework;
using FluentAssertions;

using EphemeralEx.Tests;
using System;
using System.Collections.Generic;

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
        public void SelectSuccessful_WithCorrectUsage_OnlyTakesSuccessfulElements()
        {
            var items = new int[] { 1, 2, 0, 3 };

            var result = items.SelectSuccessful<DivideByZeroException, int, int>(value => 6 / value);

            result.Should().HaveCount(3);
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