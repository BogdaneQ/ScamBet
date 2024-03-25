using Microsoft.EntityFrameworkCore;
namespace ScamBet.Entitties

{
    public class BookmacherDBContext : DbContext
    {
        public DbSet<Account> accounts { get; set; }
        public DbSet<Admin> admins { get; set; }
        public DbSet<Bet> bets { get; set; }
        public DbSet<Match> matches { get; set; }
        public DbSet<Roulette> roulettes { get; set; }
        public DbSet<Team> teams { get; set; }
        public DbSet<Team_results> teams_results { get; set; }
        public DbSet<User> users { get; set; }

        public BookmacherDBContext (DbContextOptions<BookmacherDBContext> options) : base(options)
        {

        }
        protected override void  OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = localhost; Database = ScambetBaza; User = Scambet; Password = Scambet#123321; TrustServerCertificate = true;");
        }
    }
}
