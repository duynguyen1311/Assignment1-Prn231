using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string? ProductName { get; set; }
        public double Weight { get; set; }
        public decimal UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
