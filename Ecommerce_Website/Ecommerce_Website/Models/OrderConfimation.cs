using System;
using System.Collections.Generic;

namespace Ecommerce_Website.Models;

public partial class OrderConfimation
{
    public int OrderConfimationId { get; set; }

    public int OrderId { get; set; }

    public int UserId { get; set; }

    public string Status { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
