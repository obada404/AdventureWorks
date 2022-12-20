using System;
using System.Collections.Generic;

namespace AdventureWorks.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public string? AdminName { get; set; }

    public string? Email { get; set; }

    public string? HashedPassword { get; set; }

    public string? Role { get; set; }
}
