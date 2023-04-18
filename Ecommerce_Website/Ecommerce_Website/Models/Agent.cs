using System;
using System.Collections.Generic;

namespace Ecommerce_Website.Models;

public partial class Agent
{
    public int AgentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Mobile { get; set; }

    public string? Password { get; set; }

    public string? CreatedAt { get; set; }
}
