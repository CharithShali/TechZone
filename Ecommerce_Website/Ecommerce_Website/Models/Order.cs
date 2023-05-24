using System;
using System.Collections.Generic;

namespace Ecommerce_Website.Models;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Status { get; set; }

    public double? Amount { get; set; }

    public string? AgentEmail { get; set; }

    public virtual ICollection<OrderConfimation> OrderConfimations { get; } = new List<OrderConfimation>();

    public virtual User? User { get; set; } 
}
