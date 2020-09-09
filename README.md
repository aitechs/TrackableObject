# TrackableObject

Track changes in your POCO objects

# Installation
```
PM> Install-Package AiTech.TrackableObject -Version 3.0.7.1
```

Example
---------

### To make an object trackable

Inherit class from TrackableObject
```csharp
using AiTech.Trackable;

//...

public class PersonObject : TrackableObject
{
    private string _name;
    public string Name
    {
        get => _name;
        set => SetPropertyValue<string>(ref _name, nameof(Name), value);
    }



    private string _age;
    public string Age
    {
        get => _age;
        set => SetPropertyValue<string>(ref _age, nameof(Age), value);
    }

}

```
And here is how you get to the tracked info.
```csharp
var p = new PersonObject();

    p.Name = "Jose Rizal";
    p.Age  = 20;
     
var changes = p.ChangedProperties;
foreach (var item in changes)
{
    Debug.WriteLine($"{item.Key} :: {item.Value.OldValue} ==> {item.Value.NewValue}");
}

```

To reset or clear all previous changes
```csharp
var p = new PersonObject();
    p.Name = "Jose Rizal";
    p.Age  = 20;
     
    p.StartTrackingChanges();
    
```

