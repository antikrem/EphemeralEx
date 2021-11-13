using EphemeralEx.Extensions;

using NUnit.Framework;
using FluentAssertions;


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
