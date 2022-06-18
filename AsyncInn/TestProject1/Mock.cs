using AsyncInn.Data;
using AsyncInn.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly AsyncInnDbContext _db;
        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new AsyncInnDbContext(
                new DbContextOptionsBuilder<AsyncInnDbContext>()
                    .UseSqlite(_connection)
                    .Options);

            _db.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }

        protected async Task<Amenity> CreateAndSaveTestAmenity()
        {
            var amenity = new Amenity { ID = 1, Name = "tv" };
            _db.Amenities.Add(amenity);
            await _db.SaveChangesAsync();
            Assert.NotEqual(0, amenity.ID);
            return amenity;
        }

        protected async Task<Room> CreateAndSaveTestRoom()
        {
            var room = new Room { ID = 1, Name = "rome one", Layout = 0 };
            _db.Rooms.Add(room);
            await _db.SaveChangesAsync();
            Assert.NotEqual(0, room.ID);
            return room;
        }

        protected async Task<Hotel> CreateAndSaveTestHotel()
        {
            var hotel = new Hotel { ID = 1, Name = "hotel one", Address = "amman", City ="amman", Phone ="0785214521", State ="amman"  };
            _db.Hotels.Add(hotel);
            await _db.SaveChangesAsync();
            Assert.NotEqual(0, hotel.ID);
            return hotel; ;
        }
    }
}
