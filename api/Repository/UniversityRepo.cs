using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Database;
using api.DTOs.University;
using api.Interfaces;
using api.Mappers;
using api.Models;

namespace api.Repository
{
    public class UniversityRepo : IUniversityRepository
    {
        private readonly ApplicationDBContext _context;

        public UniversityRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<University> CreateUniversityAsync(UniversityInCreate university)
        {
            var newUniversity = university.ToUniversityCreate();
            await _context.University.AddAsync(newUniversity);
            await _context.SaveChangesAsync();
            return newUniversity;
        }

        public async Task<List<University>> GetAllUniversitiesAsync()
        {
            // Include Review so the mapper can compute averages/count.
            return await _context.University.Include(u => u.Review).ToListAsync();
        }

        public async Task<University?> GetUniversityByIdAsync(Guid universityId)
        {
            return await _context.University
                .Include(u => u.Review)
                .FirstOrDefaultAsync(u => u.UniversityId == universityId);
        }
    }
}