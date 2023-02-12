namespace RoomBookingApp.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using RoomBookingApp.Domain;

    /*
     1- Startup Projects is UI (MVC, API, ... etc).

     2- Default project in package manager console is place of (ApplicationDbContext).
     */
    public class RoomBookingAppDbContext : DbContext
    {
        public RoomBookingAppDbContext(DbContextOptions<RoomBookingAppDbContext> options) : base(options)
        {

        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomBooking> RoomBookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "Conference Room A" },
                new Room { Id = 2, Name = "Conference Room B" },
                new Room { Id = 3, Name = "Conference Room C" }
            );
        }
    }
}
