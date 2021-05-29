using NUnit.Framework;

namespace SniffCore.Tests
{
    [TestFixture]
    public class WorkingIndicatorTests
    {
        [Test]
        public void IsActive_CalledWithNotDisposedIndicator_ReturnsTrue()
        {
            var indicator = new WorkingIndicator();

            var result = WorkingIndicator.IsActive(indicator);

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsActive_CalledWithDisposedIndicator_ReturnsFalse()
        {
            var indicator = new WorkingIndicator();
            indicator.Dispose();

            var result = WorkingIndicator.IsActive(indicator);

            Assert.That(result, Is.False);
        }

        [Test]
        public void IsActive_CalledNull_ReturnsFalse()
        {
            WorkingIndicator indicator = null;

            var result = WorkingIndicator.IsActive(indicator);

            Assert.That(result, Is.False);
        }
    }
}