using EphemeralEx.Reflection;

using FluentAssertions;
using NUnit.Framework;
using System;

namespace EphemeralExTests.Reflection
{
    class ExpressionHelperTests
    {
        [Test]
        public void SelectProperty_WithCorrectExpression_ReturnsCorrectReflectedInfo()
        {
            var result = ExpressionHelper.SelectProperty<ExampleClass>(example => example.Foo);

            result.Should().Match(property => property.Name == "Foo");
        }

        [Test]
        public void SelectProperty_WithIncorrectExpression_Throws()
        {
            Assert.Throws<ExpressionHelperException>(
                () => ExpressionHelper.SelectProperty<ExampleClass>(example => 12)
            );

        }

        private record ExampleClass(string Foo, object Bar, int Baz);
    }
}