//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using EntityFrameworkNet6.Data;
using EntityFrameworkNet6.Domain;
using EntityFrameworkNet6.Domain.Models;
using Microsoft.EntityFrameworkCore;

FootballLeagueDbContext context = new FootballLeagueDbContext();
/* Simple Insert Operations Methods */
//await AddNewLeague();
//await AddNewTeamsWithLeague();


/* Simple Select Queries */
//SimpleSelectQuery();

/* Queries With Filters */
//await QueryFilters();

/* Aggregate Functions */
//await AdditionalExecutionMethods();

/*Alternative LINQ Syntax*/
//await AlternativeLinqSyntax();

///* Perform Update */
//await SimpleUpdateLeagueRecord();
//await SimpleUpdateTeamRecord();

/* Perform Delete */
//await SimpleDelete();
//await DeleteWithRelationship();

/*Tracking vs No-Tracking*/
//await TrackingVsNoTracking();

/* Adding Records with relationships*/
////Adding OneToMany Related Records
//await AddNewTeamsWithLeague();
//await AddNewTeamWithLeagueId();
//await AddNewLeagueWithTeams();

////Adding ManyToMany Records
//await AddNewMatches();

////Adding OneToOne Records
//await AddNewCoach();

/* Including Related Data - Eager Loading */
//await QueryRelatedRecords();

/* Projections to Other Data Types or Ananymous Types */
//await SelectOneProperty();
//await AnonymousProjection();
//await StronglyTypedProjection();

/* Filter Based on Related Data */
await FilteringWithRelatedData();



Console.WriteLine("Press Any Key To End....");
Console.ReadKey();

async Task TrackingVsNoTracking()
{
    var withTracking = await context.Teams.FirstOrDefaultAsync(q => q.Id == 2);
    var withNoTracking = await context.Teams.AsNoTracking().FirstOrDefaultAsync(q => q.Id == 8);

    withTracking.Name = "Inter Milan";
    withNoTracking.Name = "Rivoli United";

    var entriesBeforeSave = context.ChangeTracker.Entries();

    await context.SaveChangesAsync();

    var entriesAfterSave = context.ChangeTracker.Entries();
}

async Task SimpleDelete()
{
    var league = await context.Leagues.FindAsync(24);
    context.Leagues.Remove(league);
    await context.SaveChangesAsync();

}

async Task DeleteWithRelationship()
{
    var league = await context.Leagues.FindAsync(22);
    context.Leagues.Remove(league);
    await context.SaveChangesAsync();
}

async Task GetRecord()
{
    //Retrieve Record
    var league = await context.Leagues.FindAsync(20);
    Console.WriteLine($"{league.Id} - {league.Name}");
}

async Task SimpleUpdateLeagueRecord()
{
    //Retrieve Record
    var league = await context.Leagues.FindAsync(26);

    //Make Record Change
    league.Name = "CIFA";

    //Save Changes
    await context.SaveChangesAsync();

    GetRecord();
}


async Task SimpleUpdateTeamRecord()
{
    var team = new Team
    {
        Id = 5,
        Name = "Seba United FC",
        LeagueId = 23,
    };

    context.Teams.Update(team);
    await context.SaveChangesAsync();
}

async Task AlternativeLinqSyntax()
{
    Console.Write($"Enter Team Name (Or Part Of): ");
    var teamName = Console.ReadLine();
    var teams = await (from i in context.Teams
                       where EF.Functions.Like(i.Name, $"%{teamName}%")
                       select i).ToListAsync();

    foreach (var team in teams)
    {
        Console.WriteLine($"{team.Id} - {team.Name}");
    }
}

async Task SimpleSelectQuery()
{
    //// Smartest most efficient way to get results
    var leagues = await context.Leagues.ToListAsync(); //Getting the list of leagues "The EXECUTING STATEMENT"
    foreach (var league in leagues)//Now I have the list and am searching through it
    {
        Console.WriteLine($"{league.Id} - {league.Name}");
    }

    //// Inefficient way to get results. Keeps connection open until completed and might create lock on table
    ////    ////foreach (var league in context.Leagues)
    ////    ////{
    ////    ////    Console.WriteLine($"{league.Id} - {league.Name}");
    ////    ////}
}

async Task AdditionalExecutionMethods()
{
    var leagues = context.Leagues;
    var list = await leagues.ToListAsync();
    var first = await leagues.FirstAsync();
    var firstOrDefault = await leagues.FirstOrDefaultAsync();
    //var single = await leagues.SingleAsync();
    //var singleOrDefault = await leagues.SingleOrDefaultAsync();

    //var count = await leagues.CountAsync();
    //var longCount = await leagues.LongCountAsync();
    //var min = await leagues.MinAsync();
    //var max = await leagues.MaxAsync();

    //DbSet Method that will execute
    var league = await leagues.FindAsync(1);
}

async Task QueryFilters()
{
    Console.WriteLine($"Enter League Name (Or Part Of): ");
    var leagueName = Console.ReadLine();
    var exactMatches = await context.Leagues.Where(q => q.Name.Equals(leagueName)).ToListAsync();
    foreach (var league in exactMatches)
    {
        Console.WriteLine($"{league.Id} - {league.Name}");
    }
    //var partialMatches = await context.Leagues.Where(q => q.Name.Contains(leagueName)).ToListAsync();
    var partialMatches = await context.Leagues.Where(q => EF.Functions.Like(q.Name, $"%{leagueName}%")).ToListAsync();
    foreach (var league in partialMatches)
    {
        Console.WriteLine($"{league.Id} - {league.Name}");
    }

}

async Task AddNewLeague()
{
    //Adding a new League Object
    var league = new League { Name = "Seria A" };
    await context.Leagues.AddAsync(league);
    await context.SaveChangesAsync();

    //Function to add new teams related to the new league object.
    await AddTeamsWithLeague(league);
    await context.SaveChangesAsync();
}

async Task AddTeamsWithLeague(League league)
{
    var teams = new List<Team>
    {
        new Team
        {
            Name = "Juventus",
            LeagueId = league.Id
        },

        new Team
        {
            Name = "AC Milan",
            LeagueId = league.Id
        },

        new Team
        {
            Name = "AS Roma",
            League = league
        }
    };

    await context.AddRangeAsync(teams);
}

async Task AddNewTeamsWithLeague()
{
    var league = new League { Name = "Bundesliga" };
    var team = new Team { Name = "Bayern Munich", League = league };
    await context.AddAsync(team);
    await context.SaveChangesAsync();
}
async Task AddNewTeamWithLeagueId()
{
    var team = new Team { Name = "Florentina", LeagueId = 25 };
    await context.AddAsync(team);
    await context.SaveChangesAsync();
}
async Task AddNewLeagueWithTeams()
{
    var teams = new List<Team>
    {
        new Team
        {
            Name = "Rivoli United"
        },
        new Team
        {
            Name = "Waterhouse FC"
        },
    };
    var league = new League { Name = "Florentina", Teams = teams };
    await context.AddAsync(league);
    await context.SaveChangesAsync();
}

async Task AddNewMatches()
{
    var matches = new List<Match>
    {
        new Match
        {
            AwayTeamId = 1, HomeTeamId = 2, Date = new DateTime(2022, 10, 28)
        },
        new Match
        {
            AwayTeamId = 8, HomeTeamId = 7, Date = DateTime.Now
        },
        new Match
        {
            AwayTeamId = 8, HomeTeamId = 7, Date = DateTime.Now
        }
    };
    await context.AddRangeAsync(matches);
    await context.SaveChangesAsync();
}

async Task AddNewCoach()
{
    var coach1 = new Coach { Name = "Jose Mourinho", TeamId = 3 };

    await context.AddAsync(coach1);

    var coach2 = new Coach { Name = "Antonio Conte" };

    await context.AddAsync(coach2);
    await context.SaveChangesAsync();
}
async Task QueryRelatedRecords()
{
    ////Get Many Related Records - League -> Teams
    //var leagues = await context.Leagues.Include(q => q.Teams).ToListAsync();

    ////Get One Related Record - Team -> Coach
    //var team = await context.Teams
    //    .Include(q => q.Coach)
    //    .FirstOrDefaultAsync(q => q.Id == 3);

    ////Get 'Grand Children' Related Record - Team -> Matches -> Home/Away
    //var teamWithMatchesAndOpponents = await context.Teams
    //    .Include(q => q.AwayMatches).ThenInclude(q => q.HomeTeam).ThenInclude(q => q.Coach)
    //    .Include(q => q.HomeMatches).ThenInclude(q => q.AwayTeam).ThenInclude(q => q.Coach)
    //    .FirstOrDefaultAsync(q => q.Id == 1);

    ////Get Include with filters
    //var teams = await context.Teams
    //    .Where(q => q.HomeMatches.Count > 0)
    //    .Include(q => q.Coach)
    //    .ToListAsync();
}

async Task StronglyTypedProjection()
{
    var teams = await context.Teams
        .Include(q => q.Coach)
        .Include(q => q.League)
        .Select(
        q =>
        new TeamDetail
        {
            Name = q.Name,
            CoachName = q.Coach.Name,
            LeagueName = q.League.Name,
        }
    ).ToListAsync();
    foreach (var item in teams)
    {
        Console.WriteLine($"Team: {item.Name} | Coach: {item.CoachName} | League: {item.LeagueName}");
    }
}

async Task AnonymousProjection()
{
    var teams = await context.Teams
        .Include(q => q.Coach)
        .Select(
        q =>
        new
        {
            TeamName = q.Name,
            CoachName = q.Coach.Name
        }
    ).ToListAsync();
    foreach (var item in teams)
    {
        Console.WriteLine($"Team: {item.TeamName} | Coach: {item.CoachName}");
    }
}

async Task SelectOneProperty()
{
    var teams = await context.Teams
        .Select(q => q.Name)
        .ToListAsync();
}

async Task FilteringWithRelatedData()
{
    
    var leagues = await context.Leagues.Where(q => q.Teams.Any(x => x.Name.Contains($"juv"))).ToListAsync();
    

}