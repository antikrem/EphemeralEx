using EphemeralEx.Extensions;

using NUnit.Framework;
using FluentAssertions;

using System.Collections.Generic;
using System;

namespace EphemeralExTests.Extensions
{
    public class DictionaryExtensionsTests
    {
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
