using System;

namespace Example.Models
{
    public class Product 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Supplier Supplier { get; set; }
    }
}