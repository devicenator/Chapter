using NUnit.Framework;

namespace SniffCore.Tests
{
    [TestFixture]
    public class ObjectExTests
    {
        [Test]
        public void IsNullOrEmpty_CalledOnNull_ReturnsTrue()
        {
            object item = null;

            var result = item.IsNullOrEmpty();

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsNullOrEmpty_CalledOnEmptyString_ReturnsTrue()
        {
            var item = string.Empty;

            var result = item.IsNullOrEmpty();

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsNullOrEmpty_CalledOnRandomObject_ReturnsFalse()
        {
            var item = new object();

            var result = item.IsNullOrEmpty();

            Assert.That(result, Is.False);
        }

        [Test]
        public void IsNullOrEmpty_CalledOnWhitespaceString_ReturnsFalse()
        {
            var item = "  ";

            var result = item.IsNullOrEmpty();

            Assert.That(result, Is.False);
        }

        [Test]
        public void IsNullOrEmpty_CalledOnTabString_ReturnsFalse()
        {
            var item = '\t';

            var result = item.IsNullOrEmpty();

            Assert.That(result, Is.False);
        }

        [Test]
        public void IsNullOrWhiteSpace_CalledOnNull_ReturnsTrue()
        {
            object item = null;

            var result = item.IsNullOrWhiteSpace();

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsNullOrWhiteSpace_CalledOnEmptyString_ReturnsTrue()
        {
            var item = string.Empty;

            var result = item.IsNullOrWhiteSpace();

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsNullOrWhiteSpace_CalledOnWhitespaceString_ReturnsTrue()
        {
            object item = "  ";

            var result = item.IsNullOrWhiteSpace();

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsNullOrWhiteSpace_CalledOnTabString_ReturnsTrue()
        {
            object item = '\t';

            var result = item.IsNullOrWhiteSpace();

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsNullOrWhiteSpace_CalledOnRandomObject_ReturnsFalse()
        {
            var item = new object();

            var result = item.IsNullOrWhiteSpace();

            Assert.That(result, Is.False);
        }
    }
}