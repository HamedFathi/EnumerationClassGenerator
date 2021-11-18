![code](https://user-images.githubusercontent.com/8418700/142345474-3ef91fec-380a-42e2-b95f-c6f04a8f58f5.png)

## Problem

> Enumeration or Enum types are an integral part of C# language. They have been around since the inception of language. Unfortunately, the Enum types also have some limitations. Enum types are not object-oriented. When we have to use Enums for control flow statements, behavior around Enums gets scattered across the application. We cannot inherit or extend the Enum types. These issues can especially be a deal-breaker in Domain-Driven Design (DDD).<br/> Read more [here](https://ankitvijay.net/2020/05/21/introduction-enumeration-class/) and [here](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types).

Regardless of the behaviors, there is a repetitive section where you have to write your class fields similar to what you have in a `enum` type.

```cs
// Enum
public enum CardType
{
    Amex,
    Visa,
    MasterCard
}

// Converts to

// Enumeration Class
public class CardType : Enumeration
{
    public static CardType Amex = new(1, nameof(Amex));
    public static CardType Visa = new(2, nameof(Visa));
    public static CardType MasterCard = new(3, nameof(MasterCard));

    public CardType(int id, string name)
        : base(id, name)
    {
    }
    
    // Other behaviors
}

```

## Solution

This generator helps you to focus on your business, no need to know about this pattern or details of `Enumeration` base class.

Define your `enum` type` and put `EnumerationClass` attribute on top of it.

```cs
namespace MyLibrary
{
    [EnumerationClass]
    public enum CardType
    {
        Amex,
        Visa,
        MasterCard
    }
}
```

Your enumeration class is ready and generated! before saying how to extend it, I should say how to find it!

If you use parameter-less `EnumerationClass` the default options are:

* Same `namespace` as you `enum` =>  `MyLibrary` for this sample.
* The class name will be combination of `enum` type name + "Enumeration" => `CardTypeEnumeration` for this sample.

But you can customize them:

```cs
namespace MyLibrary
{
    [EnumerationClass("MyClass", "MyNamespace")]
    public enum CardType
    {
        Amex,
        Visa,
        MasterCard
    }
}
```

The generated class is `partial` to help you add your business rules, so you can find it as following based on above setting:

```cs
// How to extend it?
// Same namespace as you defined
// Same class as you defined
// 'partial' keyword

namespace MyNamespace
    public partial class MyClass
    {
        public void MyNewMethod()
        {
            // MasterCard
        }
        
        // ...
    }
}
```

<hr/>

### [Nuget](https://www.nuget.org/packages/EnumerationClassGenerator)

```
Install-Package EnumerationClassGenerator
dotnet add package EnumerationClassGenerator
```

<hr/>

![1](https://user-images.githubusercontent.com/8418700/142345352-f5f306f3-a62d-4dc0-a9e4-d2f87dc827a5.png)

![2](https://user-images.githubusercontent.com/8418700/142345361-fb724ee1-5f47-4058-bb77-7af2d16f9fdd.png)

![3](https://user-images.githubusercontent.com/8418700/142345365-136dbf37-fd82-4a60-b03e-ae46104bb3b6.png)

<hr/>
<div>Icons made by <a href="https://www.freepik.com" title="Freepik">Freepik</a> from <a href="https://www.flaticon.com/" title="Flaticon">www.flaticon.com</a></div>


