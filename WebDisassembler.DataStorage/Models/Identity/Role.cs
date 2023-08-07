﻿using Microsoft.EntityFrameworkCore;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Models.Identity;

[PrimaryKey(nameof(Id), nameof(TenantId))]
public class Role : IIdentifiableEntity, ITenantEntity
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    
    public required string Name { get; set; }
    public required bool IsAdministrator { get; set; }
}