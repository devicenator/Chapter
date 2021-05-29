using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SniffCore.Tests
{
    [TestFixture]
    public class DelegateCommandTTests
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
            var command = new DelegateCommand<int>(x => false, x => { });

            var result = command.CanExecute(1);

            Assert.That(result, Is.False);
        }

        [Test]
        public void CanExecute_CallbackReturnsTrue_ReturnsTrue()
        {
            var command = new DelegateCommand<int>(x => true, x => { });

            var result = command.CanExecute(1);

            Assert.That(result, Is.True);
        }

        [Test]
        public void CanExecute_Called_CallbackGetsParameter()
        {
            var command = new DelegateCommand<int>(x =>
            {
                Assert.That(x, Is.EqualTo(13));
                return false;
            }, x => { });

            var result = command.CanExecute(13);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task Execute_Called_ExecutesTheCallback()
        {
            var triggered = false;
            var command = new DelegateCommand<int>(x => true, x => { triggered = true; });

            command.Execute(1);

            await Task.Delay(100);
            Assert.That(triggered, Is.True);
        }

        [Test]
        public async Task Execute_Called_CallbackGetsParameter()
        {
            var command = new DelegateCommand<int>(x => true, x => { Assert.That(x, Is.EqualTo(13)); });

            command.Execute(13);

            await Task.Delay(100);
        }

        [Test]
        public void RaiseCanExecutedChanged_Called_RaisesTheEvent()
        {
            var triggered = false;

            void CommandOnCanExecuteChanged(object sender, EventArgs e)
            {
                triggered = true;
            }

            var command = new DelegateCommand<int>(x => true, x => { });
            command.CanExecuteChanged += CommandOnCanExecuteChanged;

            command.RaiseCanExecuteChanged();

            command.CanExecuteChanged -= CommandOnCanExecuteChanged;
            Assert.That(triggered, Is.True);
        }
    }
}