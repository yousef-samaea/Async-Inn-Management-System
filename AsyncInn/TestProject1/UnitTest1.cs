using AsyncInn.Models;
using AsyncInn.Models.DTO;
using AsyncInn.Models.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1
{
    public class UnitTest1 : Mock
    {

        //protected async Task<Amenity> CreateAndSaveTestAmenity()
        //{
        //    var amenity = new Amenity { ID = 1, Name = "tv" };
        //    _db.Amenities.Add(amenity);
        //    await _db.SaveChangesAsync();
        //    Assert.NotEqual(0, amenity.ID);
        //    return amenity;
        //}

        [Fact]
        public async void TestHotel()
        {
            Hotel hotel = new Hotel {Name = "hotel one", Address = "amman", City = "amman", Phone = "0785214521", State = "amman" };
            //var repository = new HotelServices(_db);
            //hotel = await repository.Create(hotel);
            //Assert.NotEqual(0, hotel.ID);
            _db.Hotels.Add(hotel);
            await _db.SaveChangesAsync();
            Assert.NotEqual(0, hotel.ID);
        }

        [Fact]
        public async void GetRooms()
        {
            var roomservice = new RoomServieces(_db);
            List<RoomDTO> rooms = await roomservice.GetRooms();
            Assert.Equal(3, rooms.Count);
        }

        [Fact]
        public void TestGetRoom()
        {
            var RoomServieces = new RoomServieces(_db);
            RoomDTO roomDTO = new RoomDTO
            {
                Name = "room one",
                ID = 1,
                Layout = 1
            };
            var Room = RoomServieces.GetRoom(1).Result;
            Assert.Equal("rome1", Room.Name);
            Assert.NotEqual("gg", Room.Name);
        }

        [Fact]
        public async void TestDeleteRoom()
        {
            var roomServieces = new RoomServieces(_db);
            await roomServieces.DeleteRoom(1);
            var response = await roomServieces.GetRoom(1);
            Assert.Null(response);
        }

        [Fact]
        public async void TestUpdateRoom()
        {
            var roomServieces = new RoomServieces(_db);
            RoomDTO roomDTO = new RoomDTO
            {
                Name = "room one",
                ID = 1,
                Layout = 1
            };
            var room = await roomServieces.UpdateRoom(1, roomDTO);
            var room1 = roomServieces.GetRoom(1).Result;
            Assert.NotEqual("gg", room.Name);
            Assert.Equal("room one", room.Name);
        }

        [Fact]
        public async void CreateAndSaveRoomTest()
        {
            var room = new Room { Name = "room one", Layout = 0 };
            _db.Rooms.Add(room);
            await _db.SaveChangesAsync();
            Assert.NotEqual(0, room.ID);
        }


    }
}
