﻿using Microsoft.AspNetCore.Identity;

namespace StockMarket.Entities.IdentityEntities;

public class ApplicationUser: IdentityUser<Guid>
{
    public string? PersonName { get; set; }
}
