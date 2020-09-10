using System.ComponentModel;

namespace AiTech.Trackable
{
    public class TrackablePropertyChangedEventObject : PropertyChangedEventArgs 
    {
        public dynamic OldValue { get; }
        public dynamic NewValue { get; }

        public TrackablePropertyChangedEventObject(string propertyName, dynamic oldValue, dynamic newValue) : base(propertyName)
        {
            OldValue  = oldValue;
            NewValue  = newValue;
        }
    }
}