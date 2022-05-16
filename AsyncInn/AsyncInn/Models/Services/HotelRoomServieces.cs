using AsyncInn.Data;
using AsyncInn.Models.DTO;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Services
{
    public class HotelRoomServieces : IHotelRoom
    {
        private readonly AsyncInnDbContext _context;

        public HotelRoomServieces(AsyncInnDbContext context)
        {
            _context = context;
        }
        public async Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoom)
        {
            HotelRoom newHotelRoom = new HotelRoom
            {
                HotelID = hotelRoom.HotelID,
                RoomNumber = hotelRoom.RoomNumber,
                Rate = hotelRoom.Rate,
                PetFriendly = hotelRoom.PetFriendly,
                RoomID = hotelRoom.RoomID
            };
            _context.Entry(newHotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotelRoom;

        }

        public async Task DeleteHotelRoom(int hotelId, int roomNumber)
        {
            HotelRoom HotelRoom = await _context.HotelRooms.FindAsync(hotelId, roomNumber);
            _context.Entry(HotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<HotelRoomDTO> GetHotelRoom(int hotelId, int roomNumber)
        {
            return await _context.HotelRooms.Select(x => new HotelRoomDTO()
            {
                HotelID = x.HotelID,
                RoomNumber = x.RoomNumber,
                Rate = x.Rate,
                PetFriendly = x.PetFriendly,
                RoomID = x.RoomID,
                Room = new RoomDTO()
                {
                    ID = x.Room.ID,
                    Name = x.Room.Name,
                    Layout = x.Room.Layout,
                    Amenities = x.Room.RoomAmenities
                 .Select(y => new AmenityDTO()
                 {
                     ID = y.Amenities.ID,
                     Name = y.Amenities.Name
                 }).ToList()
                }
            }).FirstOrDefaultAsync(x => x.HotelID == hotelId && x.RoomNumber == roomNumber);
        }
        public async Task<List<HotelRoomDTO>> GetHotelRooms()
        {
            return await _context.HotelRooms.Select(x => new HotelRoomDTO()
            {
                HotelID = x.HotelID,
                RoomNumber = x.RoomNumber,
                Rate = x.Rate,
                PetFriendly = x.PetFriendly,
                RoomID = x.RoomID,
                Room = new RoomDTO()
                {
                    ID = x.Room.ID,
                    Name = x.Room.Name,
                    Layout = x.Room.Layout,
                    Amenities = x.Room.RoomAmenities
                 .Select(y => new AmenityDTO()
                 {
                     ID = y.Amenities.ID,
                     Name = y.Amenities.Name
                 }).ToList()
                }
            }).ToListAsync();
        }

        public async Task<HotelRoomDTO> UpdateHotelRoom(HotelRoomDTO hotelRoom)
        {
            HotelRoom updateHotelRoom = new HotelRoom
            {
                HotelID = hotelRoom.HotelID,
                RoomNumber = hotelRoom.RoomNumber,
                Rate = hotelRoom.Rate,
                PetFriendly = hotelRoom.PetFriendly,
                RoomID = hotelRoom.RoomID
            };
            _context.Entry(updateHotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }
    }
}