using System;
using System.Collections.Generic;

namespace Ecommerce_Website.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int UserId { get; set; }

    public string Ordered { get; set; } = null!;

    public string OrderedOn { get; set; } = null!;

    public virtual ICollection<CartItem> CartItems { get; } = new List<CartItem>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
