using AiTech.Trackable;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace TrackableObjectTest
{
    [TestClass]
    public class UnitTest
    {
        protected static readonly string DefaultString = "Default Name";
        protected static readonly int DefaultNumber = 1;
        protected static readonly DateTime DefaultDate = new DateTime(1920, 1, 1);

        protected static SampleTrackableObject CreateSampleObject()
        {
            return new SampleTrackableObject
            {
                StringProperty   = DefaultString,
                IntProperty      = DefaultNumber,
                DateTimeProperty = DefaultDate
            };
        }


        protected static SampleTrackableObject CreateObjectAndChangeField<T>(
            Expression<Func<SampleTrackableObject, T>> expressionField, T newValue)
        {
            var p = CreateSampleObject();

            // Use this Method to Delete All Previous Changes()
            p.StartTrackingChanges();

            var expr = (MemberExpression) expressionField.Body;
            var prop = (PropertyInfo) expr.Member;
            prop.SetValue(p, newValue, null);

            return p;
        }


        protected static IReadOnlyDictionary<string, TrackablePropertyChangedEventObject> PrintChangedValues(
            TrackableObject p)
        {
            if (p == null)
            {
                Debug.WriteLine($"Object is NULL");
                return null;
            }

            var changes = p.ChangedProperties;
            foreach (var item in changes)
            {
                Debug.WriteLine($"{item.Key} :: {item.Value.OldValue} ==> {item.Value.NewValue}");
            }

            return changes;
        }


        [TestMethod]
        public void CreateNewObjectTest()
        {
            var p = CreateSampleObject();

            Assert.IsNotNull(p);

            var changes = PrintChangedValues(p);

            Assert.AreEqual(3, changes?.Count);
        }


        [TestMethod]
        public void UpdateStringValue()
        {
            var newValue = "New String";
            var p        = CreateObjectAndChangeField(_ => _.StringProperty, newValue);

            PrintChangedValues(p);

            Assert.AreEqual(newValue, p.StringProperty);
            Assert.AreEqual(1, p.ChangedProperties.Count, "ChangedValues Count NOT equal");

            Assert.AreEqual(DefaultString, p.ChangedProperties["StringProperty"].OldValue, "Old Value NOT equal");
            Assert.AreEqual(newValue, p.ChangedProperties["StringProperty"].NewValue, "NOT equal to New Value");
        }


        [TestMethod]
        public void UpdateIntValue()
        {
            var newValue = 10;
            var p        = CreateObjectAndChangeField(_ => _.IntProperty, newValue);

            PrintChangedValues(p);

            Assert.AreEqual(newValue, p.IntProperty);
            Assert.AreEqual(1, p.ChangedProperties.Count, "ChangedValues Count NOT equal");

            Assert.AreEqual(DefaultNumber, p.ChangedProperties["IntProperty"].OldValue, "Old Value NOT equal");
            Assert.AreEqual(newValue, p.ChangedProperties["IntProperty"].NewValue, "NOT equal to New Value");
        }


        [TestMethod]
        public void UpdateDateTimeValue()
        {
            var newValue = new DateTime(2020, 12, 20);
            var p        = CreateObjectAndChangeField(_ => _.DateTimeProperty, newValue);

            PrintChangedValues(p);
            var changes = p.ChangedProperties;

           // Assert.AreEqual(newValue, p.DateTimeProperty);
           // Assert.AreEqual(1, changes.Count, "ChangedValues Count NOT equal");

            Assert.AreEqual(DefaultDate, changes["DateTimeProperty"].OldValue, "Old Value NOT equal");
            Assert.AreEqual(newValue, changes["DateTimeProperty"].NewValue, "NOT equal to New Value");
        }


        [TestMethod, TestCategory("SameValue Test")]
        public void UpdateDateTimeSameValue()
        {
            var newValue = DefaultDate;
            var p        = CreateObjectAndChangeField(_ => _.DateTimeProperty, newValue);

            PrintChangedValues(p);

            Assert.AreEqual(newValue, p.DateTimeProperty);
            Assert.AreEqual(0, p.ChangedProperties.Count, "ChangedValues Count NOT equal");
        }


        [TestMethod, TestCategory("SameValue Test")]
        public void UpdateStringSameValue()
        {
            var newValue = DefaultString;
            var p        = CreateObjectAndChangeField(_ => _.StringProperty, newValue);

            PrintChangedValues(p);

            Assert.AreEqual(newValue, p.StringProperty);
            Assert.AreEqual(0, p.ChangedProperties.Count, "ChangedValues Count NOT equal");
        }
        
    }


}