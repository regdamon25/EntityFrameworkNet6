using EntityFrameworkNet6.Data.Configurations.Entities;
using EntityFrameworkNet6.Domain;
using EntityFrameworkNet6.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkNet6.Data
{
    public class FootballLeagueDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog=FootballLeague_EfCore")
                .LogTo(Console.WriteLine, new[] {DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
                .EnableSensitiveDataLogging();
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                .HasMany(m => m.HomeMatches)
                .WithOne(m => m.HomeTeam)
                .HasForeignKey(m => m.HomeTeamId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>()
                .HasMany(m => m.AwayMatches)
                .WithOne(m => m.AwayTeam)
                .HasForeignKey(m => m.AwayTeamId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TeamsCoachesLeaguesView>().HasNoKey().ToView("TeamsCoachesLeagues");
            //    modelBuilder.ApplyConfiguration(new LeagueConfiguration());
            //    modelBuilder.ApplyConfiguration(new TeamConfiguration());
            //    modelBuilder.ApplyConfiguration(new CoachConfiguration());

        }

        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    var entries = ChangeTracker.Entries().Where(q => q.State == EntityState.Added || q.State == EntityState.Modified);

        //    foreach(var entry in entries)
        //    {
        //        var auditableObject = (BaseDomainObject)entry.Entity;
        //        auditableObject.ModifiedDate = DateTime.Now;

        //        if(entry.State == EntityState.Added)
        //        {
        //            auditableObject.CreatedDate = DateTime.Now;
        //        }
        //    }
        //    return await base.SaveChangesAsync(cancellationToken);
        //}

        public DbSet<Team> Teams { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<TeamsCoachesLeaguesView> TeamsCoachesLeagues { get; set; }
    }
}
