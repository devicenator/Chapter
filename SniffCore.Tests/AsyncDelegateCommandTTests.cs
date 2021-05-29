using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SniffCore.Tests
{
    [TestFixture]
    public class AsyncDelegateCommandTTests
    {
        [Test]
        public void Ctor_WithExecuteOnly_CanExecuteIsTrue()
        {
            var target = new AsyncDelegateCommand<int>(_ => Task.CompletedTask);

            var canExecute = target.CanExecute(1);

            Assert.That(canExecute, Is.True);
        }

        [Test]
        public void CanExecute_CallbackReturnsFalse_ReturnsFalse()
        {
            var command = new AsyncDelegateCommand<int>(x => false, x => Task.CompletedTask);

            var result = command.CanExecute(1);

            Assert.That(result, Is.False);
        }

        [Test]
        public void CanExecute_CallbackReturnsTrue_ReturnsTrue()
        {
            var command = new AsyncDelegateCommand<int>(x => true, x => Task.CompletedTask);

            var result = command.CanExecute(1);

            Assert.That(result, Is.True);
        }

        [Test]
        public void CanExecute_Called_CallbackGetsParameter()
        {
            var command = new AsyncDelegateCommand<int>(x =>
            {
                Assert.That(x, Is.EqualTo(13));
                return false;
            }, x => Task.CompletedTask);

            var result = command.CanExecute(13);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task Execute_Called_ExecutesTheCallback()
        {
            var triggered = false;
            var command = new AsyncDelegateCommand<int>(x => true, x =>
            {
                triggered = true;
                return Task.CompletedTask;
            });

            command.Execute(1);

            await Task.Delay(100);
            Assert.That(triggered, Is.True);
        }

        [Test]
        public async Task Execute_Called_CallbackGetsParameter()
        {
            var command = new AsyncDelegateCommand<int>(x => true, x =>
            {
                Assert.That(x, Is.EqualTo(13));
                return Task.CompletedTask;
            });

            command.Execute(13);

            await Task.Delay(100);
        }

        [Test]
        public async Task Execute_Called_CanExecuteIsFalseWhileExecuting()
        {
            var command = new AsyncDelegateCommand<int>(x => true, x => Task.Delay(100));
            bool? callOne = null;
            bool? callTwo = null;

            void CommandOnCanExecuteChanged(object sender, EventArgs e)
            {
                callOne ??= command.CanExecute(1);
                callTwo = command.CanExecute(1);
            }

            command.CanExecuteChanged += CommandOnCanExecuteChanged;

            command.Execute(1);

            await Task.Delay(100);
            command.CanExecuteChanged -= CommandOnCanExecuteChanged;
            Assert.That(callOne, Is.False);
            Assert.That(callTwo, Is.True);
        }

        [Test]
        public void RaiseCanExecutedChanged_Called_RaisesTheEvent()
        {
            var triggered = false;

            void CommandOnCanExecuteChanged(object sender, EventArgs e)
            {
                triggered = true;
            }

            var command = new AsyncDelegateCommand<int>(x => true, x => Task.CompletedTask);
            command.CanExecuteChanged += CommandOnCanExecuteChanged;

            command.RaiseCanExecuteChanged();

            command.CanExecuteChanged -= CommandOnCanExecuteChanged;
            Assert.That(triggered, Is.True);
        }
    }
}