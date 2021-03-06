using EphemeralEx.Serialisation;

using NUnit.Framework;
using FluentAssertions;

using System.Collections.Generic;

namespace EphemeralExTests.Serialisation
{
    public class JsonifierTests
    {
        private Jsonifier _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new Jsonifier();
        }

        [Test]
        public void Jsonify_WithString_ProperlyConverts()
        {
            var result = _sut.Jsonify("FooBar");
            result.Should().Be("\"FooBar\"");
        }

        [Test]
        public void Jsonify_WithAnonymousObject_ProperlyConverts()
        {
            var result = _sut.Jsonify(new { Name = "Foo", Age = 42 });
            result.Should().Be("{\"Name\":\"Foo\",\"Age\":42}");
        }

        [Test]
        public void Jsonify_WithConcreteObject_ProperlyConverts()
        {
            var result = _sut.Jsonify(new TestObject { Name = "Foo", Age = 42 });
            result.Should().Be("{\"Name\":\"Foo\",\"Age\":42}");
        }

        [Test]
        public void Jsonify_WithListOfConcreteObject_ProperlyConverts()
        {
            List<TestObject> list = new List<TestObject>() { new TestObject { Name = "Bar", Age = 24 }, new TestObject { Name = "Bar", Age = 24 } };
            var result = _sut.Jsonify(list);
            result.Should().Be("[{\"Name\":\"Bar\",\"Age\":24},{\"Name\":\"Bar\",\"Age\":24}]");
        }

        [Test]
        public void ParseJson_WithString_ProperlyConverts()
        {
            var result = _sut.ParseJson<string>("\"FooBar\"");
            result.Should().Be("FooBar");
        }

        [Test]
        public void ParseJson_WithConcreteObject_ProperlyConverts()
        {
            var result = _sut.ParseJson<TestObject>("{\"Name\":\"Foo\",\"Age\":42}");
            result.Should().BeEquivalentTo(new TestObject { Name = "Foo", Age = 42 });
        }

        [Test]
        public void ParseJson_WithListOfConcreteObject_ProperlyConverts()
        {
            var result = _sut.ParseJson<List<TestObject>>("[{\"Name\":\"Bar\",\"Age\":24},{\"Name\":\"Bar\",\"Age\":24}]");
            result.Should().BeEquivalentTo(new List<TestObject>() { new TestObject { Name = "Bar", Age = 24 }, new TestObject { Name = "Bar", Age = 24 } });
        }

        [Test]
        public void ParseJson_WithInvalidType_ThrowException()
        {
            _sut.Invoking(sut => sut.ParseJson<int>("\"FooBar\""))
                .Should().Throw<InvalidTypeForProvidedJson>();
        }

        private class TestObject
        {
            public string Name { get; set; }
            public int Age { get; set; }

        }
    }
}