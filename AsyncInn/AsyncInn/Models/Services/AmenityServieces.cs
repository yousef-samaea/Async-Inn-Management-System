﻿using AsyncInn.Data;
using AsyncInn.Models.DTO;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Services
{
    public class AmenityServieces : IAmenity
    {
        private AsyncInnDbContext _context;

        public AmenityServieces(AsyncInnDbContext context)
        {
            _context = context;
        }
        public async Task<AmenityDTO> Create(AmenityDTO amenity)
        {
            Amenity NewAmenity = new Amenity
            {
                ID = amenity.ID,
                Name = amenity.Name
            };
            _context.Entry(NewAmenity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return amenity;
        }

        public async Task DeleteAmenity(int Id)
        {
            AmenityDTO amenity = await GetAmenity(Id);
            _context.Entry(amenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<List<AmenityDTO>> GetAmenities()
        {
            return await _context.Amenities.Select(amenity => new AmenityDTO
            {
                ID = amenity.ID,
                Name = amenity.Name
            }).ToListAsync();
        }

        public async Task<AmenityDTO> GetAmenity(int Id)
        {
            return await _context.Amenities.Select(amenity => new AmenityDTO
            {
                ID = amenity.ID,
                Name = amenity.Name
            }).FirstOrDefaultAsync(a => a.ID == Id);
        }

        public async Task<AmenityDTO> UpdateAmenity(int Id, AmenityDTO amenity)
        {
            Amenity updateAmenity = new Amenity
            {
                ID = amenity.ID,
                Name = amenity.Name
            };
            _context.Entry(updateAmenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenity;
        }
    }
}