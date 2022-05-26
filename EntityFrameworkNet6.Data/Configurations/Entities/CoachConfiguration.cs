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
    public class CoachConfiguration : IEntityTypeConfiguration<Coach>
    {
        public void Configure(EntityTypeBuilder<Coach> builder)
        {
            builder.HasIndex(h => new { h.Name, h.TeamId}).IsUnique();
            builder.HasData(
                new Coach
                {
                    Id = 20,
                    Name = "Reggie Burrus",
                    TeamId = 20
                },
                new Coach
                {
                    Id = 21,
                    Name = "Reggie Burrus - Sample 1",
                    TeamId = 21
                },
                new Coach
                {
                    Id = 22,
                    Name = "Reggie Burrus - Sample 2",
                    TeamId = 22
                }
            );
        }
    }
}
