// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanTableTennisApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanTableTennisApp.Infrastructure.Persistence.Configurations;

public class TeamMatchConfiguration : IEntityTypeConfiguration<TeamMatch>
{
    public void Configure(EntityTypeBuilder<TeamMatch> builder)
    {
    }
}
