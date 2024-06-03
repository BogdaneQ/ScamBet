using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ScamBet.Entities
{
    public class BookmacherDBContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Roulette> Roulettes { get; set; }
        public DbSet<Team> Teams { get; set; }

        public BookmacherDBContext(DbContextOptions<BookmacherDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = localhost; Database = ScambetBaza; User = Scambet; Password = Scambet#123321; TrustServerCertificate = true;");
            }
        }
    }
}
