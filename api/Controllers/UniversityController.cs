using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.University;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityRepository _universityRepo;

        public UniversityController(IUniversityRepository universityRepository)
        {
            _universityRepo = universityRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUniversities()
        {
            var universities = await _universityRepo.GetAllUniversitiesAsync();
            var universityView = universities.Select(u => u.ToUniversityView()).ToList();
            return Ok(universityView);
        }

        [Authorize]
        [HttpGet]
        [Route("{universityId:guid}")]
        public async Task<IActionResult> GetUniversityById([FromRoute] Guid universityId)
        {
            var university = await _universityRepo.GetUniversityByIdAsync(universityId);
            if (university == null)
            {
                return NotFound();
            }

            return Ok(university.ToUniversitySingleView());
        }

        // Only Admin can create a university,everyone else is 403 Forbidden.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUniversity([FromBody] UniversityInCreate universityInCreateObject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var university = await _universityRepo.CreateUniversityAsync(universityInCreateObject);
            return CreatedAtAction(nameof(GetUniversityById), new { universityId = university.UniversityId }, university.ToUniversityView());
        }
    }
}