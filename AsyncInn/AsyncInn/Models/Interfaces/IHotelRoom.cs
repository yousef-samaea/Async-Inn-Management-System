using AsyncInn.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IHotelRoom
    {
        Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoom);
        Task<HotelRoomDTO> GetHotelRoom(int hotelId, int roomNumber);
        Task<List<HotelRoomDTO>> GetHotelRooms();
        Task<HotelRoomDTO> UpdateHotelRoom(HotelRoomDTO hotelRoom);
        Task DeleteHotelRoom(int hotelId, int roomNumber);
    }
}
