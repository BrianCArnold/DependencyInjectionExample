using System;
using System.Collections.Generic;
using Example.Interfaces;
using Example.Models;

namespace Example.SqlAccess
{
    public class EntityFrameworkMockClass : ISqlAccess
    {
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
    }
}
