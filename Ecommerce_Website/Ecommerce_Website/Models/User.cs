using System;
using System.Collections.Generic;

namespace Ecommerce_Website.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string Email { get; set; } = "";

    public string Address { get; set; } = "";

    public string Mobile { get; set; } = "";

    public string? Password { get; set; }

    public string CreatedAt { get; set; } = "";

    public string? Role { get; set; }

    public string? Token { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<Payment> Payments { get; } = new List<Payment>();

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
