using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SniffCore.Tests
{
    [TestFixture]
    public class DelegateCommandTests
    {
        [Test]
        public void Ctor_WithExecuteOnly_CanExecuteIsTrue()
        {
            var target = new AsyncDelegateCommand(() => Task.CompletedTask);

            var canExecute = target.CanExecute(null);

            Assert.That(canExecute, Is.True);
        }

        [Test]
        public void CanExecute_CallbackReturnsFalse_ReturnsFalse()
        {
            var command = new DelegateCommand(() => false, () => { });

            var result = command.CanExecute(null);

            Assert.That(result, Is.False);
        }

        [Test]
        public void CanExecute_CallbackReturnsTrue_ReturnsTrue()
        {
            var command = new DelegateCommand(() => true, () => { });

            var result = command.CanExecute(null);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task Execute_Called_ExecutesTheCallback()
        {
            var triggered = false;
            var command = new DelegateCommand(() => true, () => { triggered = true; });

            command.Execute(null);

            await Task.Delay(100);
            Assert.That(triggered, Is.True);
        }

        [Test]
        public void RaiseCanExecutedChanged_Called_RaisesTheEvent()
        {
            var triggered = false;

            void CommandOnCanExecuteChanged(object sender, EventArgs e)
            {
                triggered = true;
            }

            var command = new DelegateCommand(() => true, () => { });
            command.CanExecuteChanged += CommandOnCanExecuteChanged;

            command.RaiseCanExecuteChanged();

            command.CanExecuteChanged -= CommandOnCanExecuteChanged;
            Assert.That(triggered, Is.True);
        }
    }
}