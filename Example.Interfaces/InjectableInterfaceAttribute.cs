using System;

namespace Example.Interfaces
{
    [AttributeUsage(AttributeTargets.Interface, Inherited = false)]
    public class InjectableInterfaceAttribute : Attribute
    {
    }
}
