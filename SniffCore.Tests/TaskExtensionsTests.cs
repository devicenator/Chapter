using System.Threading.Tasks;
using NUnit.Framework;

namespace SniffCore.Tests
{
    [TestFixture]
    public class TaskExtensionsTests
    {
        [Test]
        public async Task FireAndForget_Called_ExecutesTheTask()
        {
            var triggered = false;

            async Task Executer()
            {
                triggered = true;
                await Task.CompletedTask;
            }

            Executer().FireAndForget();

            await Task.Delay(100);
            Assert.That(triggered, Is.True);
        }
    }
}