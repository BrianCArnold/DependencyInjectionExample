using System;

namespace Example.Interfaces
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class InjectableImplementationAttribute : Attribute
    {
    }
}
