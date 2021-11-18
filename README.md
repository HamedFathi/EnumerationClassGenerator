


## Problem

> Enumeration or Enum types are an integral part of C# language. They have been around since the inception of language. Unfortunately, the Enum types also have some limitations. Enum types are not object-oriented. When we have to use Enums for control flow statements, behavior around Enums gets scattered across the application. We cannot inherit or extend the Enum types. These issues can especially be a deal-breaker in Domain-Driven Design (DDD).<br/> Read more [here](https://ankitvijay.net/2020/05/21/introduction-enumeration-class/) and [here](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types).

## Solution

Regardless of the behaviors, there is a repetitive section where you have to write your class fields similar to what we have in `enum`.

```cs
// https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
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

This generator helps you to focus on your business, no need to know about this pattern or details of `Enumeration` base class.

## Usage

Define your `enum` type` and put `EnumerationClass` attribute on top of it.

```cs
namespace MyLibrary
    [EnumerationClass]
    public enum CardType
    {
        Amex,
        Visa,
        MasterCard
    }
}
```

Your enumeration class is ready and generated! before saying how to extend it I should say how to find it!

If you use parameter-less `EnumerationClass` the default options are:

* Same `namespace` as you `enum` =>  `MyLibrary` for this sample.
* The class name will be combination of `enum` type name + "Enumeration" => `CardTypeEnumeration` for this sample.

But you can customize them:

```cs
namespace MyLibrary
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




