using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace TranspotationTicketBooking.Models
{
    public partial class TicketBookingDBContext : DbContext
    {

        public TicketBookingDBContext(DbContextOptions<TicketBookingDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<BusInfo> BusInfo { get; set; }
        public DbSet<Passenger> Passenger { get; set; }
        public DbSet<Route> Route { get; set; }
        public DbSet<RouteInfo> RouteInfo { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<Ticket> Ticket { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(p => new { p.UserId, p.RoleId });
        }



    }

}