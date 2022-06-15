using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentPlanner.Data
{
    public enum PlayerNumber
    {
        Player1 = 1,
        Player2 = 2
    };

    public class TournamentPlannerDbContext : DbContext
    {
        public DbSet<Match> Matches { get; set; }

        public DbSet<Player> Players { get; set; }

        public TournamentPlannerDbContext(DbContextOptions<TournamentPlannerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Player1)
                .WithMany()
                .HasForeignKey(m => m.Player1Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Player2)
                .WithMany()
                .HasForeignKey(m => m.Player2Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Winner)
                .WithMany()
                .HasForeignKey(m => m.WinnerId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        // This class is NOT COMPLETE.
        // Todo: Complete the class according to the requirements

        /// <summary>
        /// Adds a new player to the player table
        /// </summary>
        /// <param name="newPlayer">Player to add</param>
        /// <returns>Player after it has been added to the DB</returns>
        public async Task<Player> AddPlayer(Player newPlayer)
        {
            Players.Add(newPlayer);
            await SaveChangesAsync();
            return newPlayer;
        }

        /// <summary>
        /// Adds a match between two players
        /// </summary>
        /// <param name="player1Id">ID of player 1</param>
        /// <param name="player2Id">ID of player 2</param>
        /// <param name="round">Number of the round</param>
        /// <returns>Generated match after it has been added to the DB</returns>
        public async Task<Match> AddMatch(int player1Id, int player2Id, int round)
        {
            var player1 = Players.FirstOrDefault(p => p.ID == player1Id);
            var player2 = Players.FirstOrDefault(p => p.ID == player2Id);
            var newMatch = new Match()
            {
                Player1 = player1,
                Player2 = player2,
            };
            Matches.Add(newMatch);
            await SaveChangesAsync();
            return newMatch;
        }

        /// <summary>
        /// Set winner of an existing game
        /// </summary>
        /// <param name="matchId">ID of the match to update</param>
        /// <param name="player">Player who has won the match</param>
        /// <returns>Match after it has been updated in the DB</returns>
        public async Task<Match> SetWinner(int matchId, PlayerNumber player)
        {
            var match = Matches.FirstOrDefault(m => m.ID == matchId);
            switch (player)
            {
                case PlayerNumber.Player1:
                    match.Winner = match.Player1;
                    break;
                case PlayerNumber.Player2:
                    match.Winner = match.Player2;
                    break;
            }

            Matches.Update(match);
            await SaveChangesAsync();
            return match;
        }

        /// <summary>
        /// Get a list of all matches that do not have a winner yet
        /// </summary>
        /// <returns>List of all found matches</returns>
        public async Task<IList<Match>> GetIncompleteMatches()
        {
            var incompleteMatches = await Matches
                .Where(m => m.Winner == null)
                .ToListAsync();
            return incompleteMatches;
        }

        /// <summary>
        /// Delete everything (matches, players)
        /// </summary>
        public async Task DeleteEverything()
        {
            Matches.RemoveRange(await Matches.ToArrayAsync());
            Players.RemoveRange(await Players.ToArrayAsync());
            await SaveChangesAsync();
        }

        /// <summary>
        /// Get a list of all players whose name contains <paramref name="playerFilter"/>
        /// </summary>
        /// <param name="playerFilter">Player filter. If null, all players must be returned</param>
        /// <returns>List of all found players</returns>
        public async Task<IList<Player>> GetFilteredPlayers(string? playerFilter = null)
        {
            return await Players
                .Where(p => playerFilter == null || p.Name.Contains(playerFilter))
                .ToListAsync();
        }

        /// <summary>
        /// Generate match records for the next round
        /// </summary>
        /// <exception cref="InvalidOperationException">Error while generating match records</exception>
        public async Task GenerateMatchesForNextRound()
        {
            using var transaction = await Database.BeginTransactionAsync();

            if ((await GetIncompleteMatches()).Any()) throw new InvalidOperationException("Incomplete Matches");

            var players = await GetFilteredPlayers();
            if (players.Count != 32) throw new InvalidOperationException("Incorrect number of players");

            var numberOfMatches = await Matches.CountAsync();
            switch (numberOfMatches)
            {
                case 0:
                    AddFirstRound(Matches, players);
                    break;
                case var n when n is 16 or 24 or 28 or 30:
                    await AddSubsequentRound(Matches);
                    break;
                default:
                    throw new InvalidOperationException("Invalid number of rounds");
            }

            await SaveChangesAsync();
            await transaction.CommitAsync();

            static void AddFirstRound(DbSet<Match> matches, IList<Player> players)
            {
                var rand = new Random();

                for (var i = 0; i < 16; i++)
                {
                    var player1 = players[rand.Next(players.Count)];
                    players.Remove(player1);
                    var player2 = players[rand.Next(players.Count)];
                    players.Remove(player2);
                    matches.Add(new Match
                    {
                        Player1 = player1,
                        Player2 = player2,
                        Round = 1,
                    });
                }
            }

            static async Task AddSubsequentRound(DbSet<Match> matches)
            {
                var rand = new Random();

                var prevRound = await matches.MaxAsync(m => m.Round);
                var prevRoundMatches = await matches.Where(m => m.Round == prevRound).ToListAsync();
                var nextRound = prevRound + 1;
                for (var i = prevRoundMatches.Count / 2; i > 0; i--)
                {
                    var match1 = prevRoundMatches[rand.Next(prevRoundMatches.Count)];
                    prevRoundMatches.Remove(match1);
                    var match2 = prevRoundMatches[rand.Next(prevRoundMatches.Count)];
                    prevRoundMatches.Remove(match2);
                    matches.Add(new Match
                    {
                        Player1 = match1.Winner,
                        Player2 = match2.Winner,
                        Round = nextRound,
                    });
                }
            }
        }
    }
}