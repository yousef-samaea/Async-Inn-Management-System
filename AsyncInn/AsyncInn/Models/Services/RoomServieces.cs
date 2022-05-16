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
    public class RoomServieces : IRoom
    {
        private readonly AsyncInnDbContext _context;

        public RoomServieces(AsyncInnDbContext context)
        {
            _context = context;
        }
        public async Task<RoomDTO> Create(RoomDTO room)
        {
            Room newRoom = new Room
            {
                ID = room.ID,
                Name = room.Name,
                Layout = room.Layout
            };
            _context.Entry(newRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task DeleteRoom(int Id)
        {
            Room room = await _context.Rooms.FindAsync(Id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<RoomDTO> GetRoom(int Id)
        {
            return await _context.Rooms.Select(x => new RoomDTO
            {
                ID = x.ID,
                Name = x.Name,
                Layout = x.Layout,
                Amenities = x.RoomAmenities
                .Select(y => new AmenityDTO
                {
                    ID = y.Amenities.ID,
                    Name = y.Amenities.Name
                }).ToList()
            }).FirstOrDefaultAsync(x => x.ID == Id);
        }

        public async Task<List<RoomDTO>> GetRooms()
        {
            return await _context.Rooms.Select(x => new RoomDTO
            {
                ID = x.ID,
                Name = x.Name,
                Layout = x.Layout,
                Amenities = x.RoomAmenities
                .Select(y => new AmenityDTO
                {
                    ID = y.Amenities.ID,
                    Name = y.Amenities.Name
                }).ToList()
            }).ToListAsync();

        }


        public async Task<RoomDTO> UpdateRoom(int ID, RoomDTO room)
        {
            Room UpdateRoom = new Room
            {
                ID = room.ID,
                Name = room.Name,
                Layout = room.Layout
            };
            _context.Entry(UpdateRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return room;
        }
        //logic to add and remove amenities from rooms

        public async Task AddAmenityToRoom(int roomId, int amenityId)
        {
            RoomAmenities roomAmenity = new RoomAmenities()
            {
                RoomID = roomId,
                AmenitiesID = amenityId
            };

            _context.Entry(roomAmenity).State = EntityState.Added;

            await _context.SaveChangesAsync();
        }
        public async Task RemoveAmentityFromRoom(int roomId, int amenityId)
        {
            var removeAmentity = _context.RoomAmenities.FirstOrDefaultAsync(x => x.RoomID == roomId && x.AmenitiesID == amenityId);

            _context.Entry(removeAmentity).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }
    }
}
