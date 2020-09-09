using System.ComponentModel;

namespace AiTech.Trackable
{
    public class TrackablePropertyChangedEventObject : PropertyChangedEventArgs 
    {
        public string FieldName { get; }

        public dynamic OldValue { get; }
        public dynamic NewValue { get; }

        public TrackablePropertyChangedEventObject(string propertyName, dynamic oldValue, dynamic newValue) : base(propertyName)
        {
            FieldName = propertyName;
            OldValue  = oldValue;
            NewValue  = newValue;
        }
    }
}