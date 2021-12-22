using EphemeralEx.Extensions;

using System.Linq;

using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;

namespace EphemeralExTests.Extensions
{
    class ExpressionExtensionsTests
    {
        [Test]
        public void Do_WithAction_CallsActionAndReturnsSameInstance()
        {
            var obj = new TestInlineActionClass();

            var result = obj.Do(o => o.Act());

            obj.ActedOn.Should().BeTrue();
            result.Should().BeSameAs(obj);
        }

        [Test]
        public void Collect_WithFunctions_ReturnsCorrectObject()
        {
            var result = 2.Collect(
                    v => 1 * v,
                    v => 2 * v,
                    v => 3 * v
                );

            result.Should().BeEquivalentTo(new List<int> { 2, 4, 6});
        }

        [TestCase(2.45, 1)]
        [TestCase(0, 0)]
        [TestCase(-4.2, -1)]
        public void Collect_ForMatching_Works(double value, int output)
        {
            var result = value.Collect<double, int?>(
                    v => v > 0 ? 1 : null,
                    v => v == 0 ? 0 : null,
                    v => v < 0 ? -1 : null
                ).NotNull().Single();

            result.Should().Be(output);
        }

        private class TestInlineActionClass
        {
            public bool ActedOn { get; set; } = false;

            public void Act()
            {
                ActedOn = true;
            }
        }
    }
}