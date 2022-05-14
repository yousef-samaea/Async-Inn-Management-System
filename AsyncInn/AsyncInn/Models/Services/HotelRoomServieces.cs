using AsyncInn.Data;
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

        public async Task<HotelRoom> Create(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }

        public async Task DeleteHotelRoom(int hotelId, int roomNumber)
        {
            HotelRoom HotelRoom = await GetHotelRoom(hotelId, roomNumber);
            _context.Entry(HotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync(); ;
        }

        public async Task<HotelRoom> GetHotelRoom(int hotelId, int roomNumber)
        {
            return await _context.HotelRooms
                           .Where(h => h.HotelID == hotelId && h.RoomNumber == roomNumber)
                           .FirstOrDefaultAsync();
        }

        public async Task<List<HotelRoom>> GetHotelRooms(int hotelId)
        {
            return await _context.HotelRooms
                                   .Where(h => h.HotelID == hotelId)
                                   .ToListAsync();
        }

        public async Task<HotelRoom> UpdateHotelRoom(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }

        public async Task AddRoomToHotel(int roomId, int hotelId)
        {
            HotelRoom HotelRoom = new HotelRoom()
            {
                RoomID = roomId,
                HotelID = hotelId,
                Room = await _context.Rooms.FirstOrDefaultAsync(r => r.ID == roomId),
                Hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.ID == hotelId)

            };

            _context.Entry(HotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRoomFromHotel(int roomNumber, int hotelId)
        {
            var result = await _context.HotelRooms.FirstOrDefaultAsync(x => x.RoomNumber == roomNumber && x.HotelID == hotelId);

            _context.Entry(result).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }



    }
}
