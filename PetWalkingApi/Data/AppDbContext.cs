using Microsoft.EntityFrameworkCore;
using PetWalkingApi.Models;

namespace PetWalkingApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Tablas
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
        public DbSet<PetBreed> PetBreeds { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<AvailabilitySlot> AvailabilitySlots { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ClientPayment> ClientPayments { get; set; }
        public DbSet<WalkerPayment> WalkerPayments { get; set; }
        public DbSet<Profit> Profits { get; set; }
        public DbSet<Zone> Zones { get; set; }
    }
}
