using System;
using System.Collections.Generic;

namespace Ecommerce_Website.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int CategoryId { get; set; }

    public double Price { get; set; }

    public int Quantity { get; set; }

    public string ImageName { get; set; } = null!;

    public int? UserId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; } = new List<CartItem>();

    public virtual ProductCategory? Category { get; set; } 

    public virtual User? User { get; set; }
}
