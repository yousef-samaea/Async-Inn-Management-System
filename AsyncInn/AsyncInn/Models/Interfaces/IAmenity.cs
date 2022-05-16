using AsyncInn.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IAmenity
    {
        Task<AmenityDTO> Create(AmenityDTO amenity);
        Task<AmenityDTO> GetAmenity(int Id);
        Task<List<AmenityDTO>> GetAmenities();
        Task<AmenityDTO> UpdateAmenity(int Id, AmenityDTO amenity);
        Task DeleteAmenity(int Id);
    }
}
