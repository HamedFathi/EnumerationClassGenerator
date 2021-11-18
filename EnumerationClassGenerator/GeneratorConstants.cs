namespace EnumerationClassGenerator
{
    internal static class GeneratorConstants
    {
        internal const string EnumerationClassAttribute = @"
            namespace System
            {
                [AttributeUsage(AttributeTargets.Enum, Inherited = false, AllowMultiple = false)]
                public sealed class EnumerationClassAttribute : Attribute
                {
                    public EnumerationClassAttribute(string @class = null, string @namespace = null)
                    {
                    }

                }
            }";
    }
}
