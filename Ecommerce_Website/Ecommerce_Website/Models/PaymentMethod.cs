using System;
using System.Collections.Generic;

namespace Ecommerce_Website.Models;

public partial class PaymentMethod
{
    public int PaymentMethodId { get; set; }

    public string? Type { get; set; }

    public string? Provider { get; set; }

    public string? Available { get; set; }

    public string? Reason { get; set; }

    public virtual ICollection<Payment> Payments { get; } = new List<Payment>();
}
