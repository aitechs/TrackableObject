using System.Collections.Generic;
using System.ComponentModel;

namespace AiTech.Trackable
{
    public abstract class TrackableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Dictionary<string, TrackablePropertyChangedEventObject> _changes;

        public bool IsDirty { get; set; }

        protected TrackableObject()
        {
            IsDirty  = false;
            _changes = new Dictionary<string, TrackablePropertyChangedEventObject>();
        }

        public IReadOnlyDictionary<string, TrackablePropertyChangedEventObject> ChangedProperties => _changes;


        public virtual void StartTrackingChanges()
        {
            IsDirty = false;
            _changes.Clear();
        }


        protected bool SetPropertyValue<T>(ref T field, string nameOfProperty, T value)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            var oldValue = field;
            field   = value;
            IsDirty = true;

            OnPropertyChanged(this, new TrackablePropertyChangedEventObject(nameOfProperty, oldValue, value));
            return true;
        }


        protected virtual void OnPropertyChanged(object sender, TrackablePropertyChangedEventObject arg)
        {
            AddOrUpdateChanges(arg);
            PropertyChanged?.Invoke(this, arg);
        }

        protected void AddOrUpdateChanges(TrackablePropertyChangedEventObject arg)
        {
            if (!_changes.TryGetValue(arg.PropertyName, out _))
                _changes.Add(arg.PropertyName, arg);
            else
                _changes[arg.PropertyName] = arg;
        }
    }
}