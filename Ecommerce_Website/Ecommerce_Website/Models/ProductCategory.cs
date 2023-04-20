using System;
using System.Collections.Generic;

namespace Ecommerce_Website.Models;

public partial class ProductCategory
{
    public int CategoryId { get; set; }

    public string Category { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
