using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DataAccess.Entity;
using DataAccess.Configs;

namespace DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<StatusSession> StatusSessions { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<StatusTicket> StatusTickets { get; set; }
        public DbSet<User> Users { get; set; } 
        public DbSet<Sale> Sales{ get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<RegularDiscount> RegularDiscounts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FilmConfiguration());
            modelBuilder.ApplyConfiguration(new HallConfiguration());
            modelBuilder.ApplyConfiguration(new SessionConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
            modelBuilder.ApplyConfiguration(new SaleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
