using System;


namespace EnumerationClassGenerator.Sample
{
    [EnumerationClass(/*"MyClass", "MyNamespace"*/)]
    public enum CardType
    {
        Amex,
        Visa,
        MasterCard
    }

    public partial class CardTypeEnumeration
    {
        public void MyNewMethod()
        {
            
        }
    }
}