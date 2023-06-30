using CleanTableTennisApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanTableTennisApp.Infrastructure.Persistence.Configurations;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.Property(s => s.Name)
            .IsRequired();

    }
}

public class TeamMatchConfiguration : IEntityTypeConfiguration<TeamMatch>
{
    public void Configure(EntityTypeBuilder<TeamMatch> builder)
    {
    }
}