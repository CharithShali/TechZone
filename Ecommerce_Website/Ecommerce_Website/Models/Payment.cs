using System;
using System.Collections.Generic;

namespace Ecommerce_Website.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PaymentMethodId { get; set; }

    public int TotalAmount { get; set; }

    public string CreatedAt { get; set; } = null!;

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
