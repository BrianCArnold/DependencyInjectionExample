using System.Collections.Generic;
using Example.Models;
using Microsoft.Extensions.Hosting;

namespace Example.Interfaces
{
    [InjectableInterface]
    public interface ISqlAccess
    {
        ICollection<Product> Products { get; set; }
        ICollection<Supplier> Suppliers { get; set; }
    }
}