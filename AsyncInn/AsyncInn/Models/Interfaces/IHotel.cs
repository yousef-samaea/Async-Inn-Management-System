using AsyncInn.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IHotel
    {
        Task<HotelDTO> Create(HotelDTO hotel);
        Task<HotelDTO> GetHotel(int Id);
        Task<List<HotelDTO>> GetHotels();
        Task<HotelDTO> UpdateHotel(int Id, HotelDTO hotel);
        Task DeleteHotel(int Id);
    }
}
