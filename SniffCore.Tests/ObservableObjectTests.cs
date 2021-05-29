using System.ComponentModel;
using NUnit.Framework;

namespace SniffCore.Tests
{
    [TestFixture]
    public class ObservableObjectTests
    {
        [SetUp]
        public void Setup()
        {
            _target = new ObservableObjectTestClass();
        }

        private ObservableObjectTestClass _target;

        [Test]
        public void NotifyPropertyChanging_Called_RaisesPropertyChanging()
        {
            var triggered = false;

            void TargetOnPropertyChanging(object sender, PropertyChangingEventArgs e)
            {
                triggered = e.PropertyName == "propertyName";
            }

            _target.PropertyChanging += TargetOnPropertyChanging;

            _target.CallNotifyPropertyChanging("propertyName");

            _target.PropertyChanging -= TargetOnPropertyChanging;
            Assert.That(triggered, Is.True);
        }

        [Test]
        public void NotifyPropertyChanged_Called_RaisesPropertyChanged()
        {
            var triggered = false;

            void TargetOnPropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                triggered = e.PropertyName == "propertyName";
            }

            _target.PropertyChanged += TargetOnPropertyChanged;

            _target.CallNotifyPropertyChanged("propertyName");

            _target.PropertyChanged -= TargetOnPropertyChanged;
            Assert.That(triggered, Is.True);
        }

        [Test]
        public void NotifyAndSet_Called_RaisesPropertyChangingBeforeBackingField()
        {
            var triggered = false;
            var backingField = string.Empty;

            void TargetOnPropertyChanging(object sender, PropertyChangingEventArgs e)
            {
                triggered = e.PropertyName == "propertyName";
                Assert.That(backingField, Is.Empty);
            }

            _target.PropertyChanging += TargetOnPropertyChanging;

            _target.CallNotifyAndSet(ref backingField, "NewValue", "propertyName");

            _target.PropertyChanging -= TargetOnPropertyChanging;
            Assert.That(triggered, Is.True);
        }

        [Test]
        public void NotifyAndSet_Called_SetsTheBackingField()
        {
            var backingField = string.Empty;

            _target.CallNotifyAndSet(ref backingField, "NewValue", "propertyName");

            Assert.That(backingField, Is.EqualTo("NewValue"));
        }

        [Test]
        public void NotifyAndSet_Called_RaisesPropertyChangedAfterBackingField()
        {
            var triggered = false;
            var backingField = string.Empty;

            void TargetOnPropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                triggered = e.PropertyName == "propertyName";
                Assert.That(backingField, Is.EqualTo("NewValue"));
            }

            _target.PropertyChanged += TargetOnPropertyChanged;

            _target.CallNotifyAndSet(ref backingField, "NewValue", "propertyName");

            _target.PropertyChanged -= TargetOnPropertyChanged;
            Assert.That(triggered, Is.True);
        }

        [Test]
        public void NotifyAndSetIfChanged_CalledWithoutChangedValue_DoesNotRaisePropertyChanging()
        {
            var backingField = "Value";

            void TargetOnPropertyChanging(object sender, PropertyChangingEventArgs e)
            {
                Assert.Fail("PropertyChanging unexpected");
            }

            _target.PropertyChanging += TargetOnPropertyChanging;

            _target.CallNotifyAndSetIfChanged(ref backingField, "Value", "propertyName");

            _target.PropertyChanging -= TargetOnPropertyChanging;
        }

        [Test]
        public void NotifyAndSetIfChanged_CalledWithoutChangedValue_DoesNotRaisePropertyChanged()
        {
            var backingField = "Value";

            void TargetOnPropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                Assert.Fail("PropertyChanged unexpected");
            }

            _target.PropertyChanged += TargetOnPropertyChanged;

            _target.CallNotifyAndSetIfChanged(ref backingField, "Value", "propertyName");

            _target.PropertyChanged -= TargetOnPropertyChanged;
        }

        [Test]
        public void NotifyAndSetIfChanged_CalledWitChangedValue_RaisesPropertyChangingBeforeBackingField()
        {
            var triggered = false;
            var backingField = string.Empty;

            void TargetOnPropertyChanging(object sender, PropertyChangingEventArgs e)
            {
                triggered = e.PropertyName == "propertyName";
                Assert.That(backingField, Is.Empty);
            }

            _target.PropertyChanging += TargetOnPropertyChanging;

            _target.CallNotifyAndSetIfChanged(ref backingField, "NewValue", "propertyName");

            _target.PropertyChanging -= TargetOnPropertyChanging;
            Assert.That(triggered, Is.True);
        }

        [Test]
        public void NotifyAndSetIfChanged_CalledWitChangedValue_SetsTheBackingField()
        {
            var backingField = string.Empty;

            _target.CallNotifyAndSetIfChanged(ref backingField, "NewValue", "propertyName");

            Assert.That(backingField, Is.EqualTo("NewValue"));
        }

        [Test]
        public void NotifyAndSetIfChanged_CalledWitChangedValue_RaisesPropertyChangedAfterBackingField()
        {
            var triggered = false;
            var backingField = string.Empty;

            void TargetOnPropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                triggered = e.PropertyName == "propertyName";
                Assert.That(backingField, Is.EqualTo("NewValue"));
            }

            _target.PropertyChanged += TargetOnPropertyChanged;

            _target.CallNotifyAndSetIfChanged(ref backingField, "NewValue", "propertyName");

            _target.PropertyChanged -= TargetOnPropertyChanged;
            Assert.That(triggered, Is.True);
        }

        private class ObservableObjectTestClass : ObservableObject
        {
            public void CallNotifyPropertyChanging(string propertyName)
            {
                NotifyPropertyChanging(propertyName);
            }

            public void CallNotifyPropertyChanged(string propertyName)
            {
                NotifyPropertyChanged(propertyName);
            }

            public void CallNotifyAndSet<T>(ref T backingField, T newValue, string propertyName)
            {
                NotifyAndSet(ref backingField, newValue, propertyName);
            }

            public void CallNotifyAndSetIfChanged<T>(ref T backingField, T newValue, string propertyName)
            {
                NotifyAndSetIfChanged(ref backingField, newValue, propertyName);
            }
        }
    }
}