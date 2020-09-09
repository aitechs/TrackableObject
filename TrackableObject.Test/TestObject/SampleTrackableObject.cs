using AiTech.Trackable;
using System;

namespace TrackableObjectTest
{
    public class SampleTrackableObject : TrackableObject
    {
        private string _stringProperty;
        public string StringProperty
        {
            get => _stringProperty;
            set => SetPropertyValue<string>(ref _stringProperty, nameof(StringProperty), value);
        }



        private int _intProperty;
        public int IntProperty
        {
            get => _intProperty;
            set => SetPropertyValue<int>(ref _intProperty, nameof(IntProperty), value);
        }



        private DateTime _dateTimeProperty;
        public DateTime DateTimeProperty
        {
            get => _dateTimeProperty;
            set => SetPropertyValue<DateTime>(ref _dateTimeProperty, nameof(DateTimeProperty), value);
        }



       
    }
}