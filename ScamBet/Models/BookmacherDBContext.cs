using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScamBet.Models;

namespace ScamBet.Entities
{
    public class BookmacherDBContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Roulette> Roulette { get; set; }
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { role_ID = (int)RoleType.User, RoleName = RoleType.User.ToString() },
                new Role { role_ID = (int)RoleType.Admin, RoleName = RoleType.Admin.ToString()}
            );

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Role)
                .WithMany(r => r.Accounts)
                .HasForeignKey(a => a.role_ID);
        }
    }
}
