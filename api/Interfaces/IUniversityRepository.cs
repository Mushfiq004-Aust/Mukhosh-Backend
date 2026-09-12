using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.University;
using api.Models;

namespace api.Interfaces
{
    public interface IUniversityRepository
    {
        Task<List<University>> GetAllUniversitiesAsync();
        Task<University?> GetUniversityByIdAsync(Guid universityId);
        Task<University> CreateUniversityAsync(UniversityInCreate university);
    }
}