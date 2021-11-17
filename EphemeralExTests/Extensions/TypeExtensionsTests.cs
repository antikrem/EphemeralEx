using EphemeralEx.Extensions;

using FluentAssertions;
using NUnit.Framework;

using System.Collections.Generic;
using System.Linq;


namespace EphemeralExTests.Extensions
{
    class TypeExtensionsTests
    {
        [Test]
        public void Inherits_WithUnrelatedType_ReturnsFalse()
        {
            var result = typeof(Unrelated).Inherits<IBase>();

            result.Should().Be(false);
        }

        [Test]
        public void Inherits_WithImplementation_ReturnsTrue()
        {
            var result = typeof(Implementation).Inherits<IBase>();

            result.Should().Be(true);
        }

        [Test]
        public void Inherits_WithSubType_ReturnsTrue()
        {
            var result = typeof(Extension).Inherits<Implementation>();

            result.Should().Be(true);
        }

        [Test]
        public void Inherits_WithSameType_ReturnsFalse()
        {
            var result = typeof(Extension).Inherits<Extension>();

            result.Should().Be(false);
        }

        private interface IBase {};
        private class Implementation : IBase {};
        private class Extension : Implementation { };
        private class Unrelated { };

    }
}
