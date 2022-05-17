using AsyncInn.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Data
{
    public class AsyncInnDbContext : IdentityDbContext<ApplicationUser>
    {
        public AsyncInnDbContext(DbContextOptions<AsyncInnDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HotelRoom>().HasKey(x => new { x.HotelID, x.RoomNumber });
            modelBuilder.Entity<RoomAmenities>().HasKey(x => new { x.AmenitiesID, x.RoomID});


            modelBuilder.Entity<Hotel>().HasData(
  new Hotel { ID=1, Address="amman", City="amman", Name="samara", Phone="123", State="kk" },
  new Hotel { ID = 2, Address = "amman", City = "amman", Name = "samara1", Phone = "123", State = "kk" },
  new Hotel { ID = 3, Address = "amman", City = "amman", Name = "samara2", Phone = "123", State = "kk" }

);

            modelBuilder.Entity<Room>().HasData(
                new Room { ID=1, Name="rome1", Layout=1},
                new Room { ID = 2, Name = "rome2", Layout = 2 },
                new Room { ID = 3, Name = "rome2", Layout = 3 }
                );

            modelBuilder.Entity<Amenity>()
                .HasData(new Amenity { ID = 1, Name = "pool" },
                         new Amenity { ID = 2, Name = "drinks" },
                         new Amenity { ID = 3, Name = "food" },
                         new Amenity { ID = 4, Name = "music" }
                         );

        }

        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<RoomAmenities> RoomAmenities { get; set; }

        public DbSet<AsyncInn.Models.DTO.UserDTO> UserDTO { get; set; }
    }

}
