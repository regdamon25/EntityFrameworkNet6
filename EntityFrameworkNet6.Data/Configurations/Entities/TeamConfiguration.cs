using EntityFrameworkNet6.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkNet6.Data.Configurations.Entities
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasIndex(h => h.Name).IsUnique();

            //builder.HasMany(m => m.HomeMatches) //Team has many home matches
            //    .WithOne(m => m.HomeTeam) // Team has many home matches with ONE Home Team
            //    .HasForeignKey(m => m.HomeTeamId) // It has a foreign key with Home Team ID
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.HasMany(m => m.AwayMatches)
            //    .WithOne(m => m.AwayTeam)
            //    .HasForeignKey(m => m.AwayTeamId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                  new Team
                  {
                      Id = 20,
                      Name = "Reggie Burrus - Sample Team 1",
                      LeagueId = 20
                  },
                  new Team
                  {
                      Id = 21,
                      Name = "Reggie Burrus - Sample Team 2",
                      LeagueId = 20

                  },
                  new Team
                  {
                      Id = 22,
                      Name = "Reggie Burrus - Sample Team 3",
                      LeagueId = 20

                  }
              );

        }
    }
}
