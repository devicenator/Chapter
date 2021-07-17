using NUnit.Framework;
using SniffCore.Tests.Subnamespace;

namespace SniffCore.Tests
{
    [TestFixture]
    public class NameOfTests
    {
        [Test]
        public void NameT_CalledWithoutProperty_ReturnsSameAsNameof()
        {
            var expected = "Second";

            var actual = NameOf.Name<Second>();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Name_CalledWithoutProperty_ReturnsSameAsNameof()
        {
            var expected = "Second";

            var actual = NameOf.Name(typeof(Second));

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void NameT_CalledWithProperty_ReturnsSameAsNameof()
        {
            var expected = "Second.Name";

            var actual = NameOf.Name<Second>(nameof(Second.Name));

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Name_CalledWithProperty_ReturnsSameAsNameof()
        {
            var expected = "Second.Name";
            
            var actual = NameOf.Name(typeof(Second), nameof(Second.Name));

            Assert.That(actual, Is.EqualTo(expected));
        }
        
        [Test]
        public void NamespaceT_Called_ReturnsSameAsNameof()
        {
            var expected = "SniffCore.Tests.Subnamespace";

            var actual = NameOf.Namespace<Second>();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Namespace_Called_ReturnsSameAsNameof()
        {
            var expected = "SniffCore.Tests.Subnamespace";

            var actual = NameOf.Namespace(typeof(Second));

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void NamespaceT_CalledWithTwoEqualTypes_ReturnsEmpty()
        {
            var expected = string.Empty;

            var actual = NameOf.Namespace<First, First>();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void NamespaceT_CalledWithTwoDifferentTypes_Relatives1()
        {
            var expected = "Subnamespace";

            var actual = NameOf.Namespace<First, Second>();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void NamespaceT_CalledWithTwoDifferentTypes_Relatives2()
        {
            var expected = "Subnamespace";

            var actual = NameOf.Namespace<Second, First>();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Namespace_CalledWithTwoEqualTypes_ReturnsEmpty()
        {
            var expected = string.Empty;

            var actual = NameOf.Namespace(typeof(First), typeof(First));

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Namespace_CalledWithTwoDifferentTypes_Relatives1()
        {
            var expected = "Subnamespace";

            var actual = NameOf.Namespace(typeof(First), typeof(Second));

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Namespace_CalledWithTwoDifferentTypes_Relatives2()
        {
            var expected = "Subnamespace";

            var actual = NameOf.Namespace(typeof(Second), typeof(First));

            Assert.That(actual, Is.EqualTo(expected));
        }
        
        [Test]
        public void FullNameT_CalledWithoutProperty_ReturnsSameAsNameof()
        {
            var expected = "SniffCore.Tests.Subnamespace.Second";

            var actual = NameOf.FullName<Second>();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void FullName_CalledWithoutProperty_ReturnsSameAsNameof()
        {
            var expected = "SniffCore.Tests.Subnamespace.Second";

            var actual = NameOf.FullName(typeof(Second));

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void FullNameT_CalledWithProperty_ReturnsSameAsNameof()
        {
            var expected = "SniffCore.Tests.Subnamespace.Second.Name";

            var actual = NameOf.FullName<Second>(nameof(Second.Name));

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void FullName_CalledWithProperty_ReturnsSameAsNameof()
        {
            var expected = "SniffCore.Tests.Subnamespace.Second.Name";

            var actual = NameOf.FullName(typeof(Second), nameof(Second.Name));

            Assert.That(actual, Is.EqualTo(expected));
        }
    }

    public class First
    {
        public string Name { get; set; }
    }

    namespace Subnamespace
    {
        public class Second
        {
            public string Name { get; set; }
        }
    }
}