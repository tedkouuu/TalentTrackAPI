using Microsoft.AspNetCore.Mvc;
using TalentTrackAPI.Models;
using TalentTrackAPI.Repositories;

namespace TalentTrackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobPositionsController : ControllerBase
    {
        private readonly IRepository<JobPosition> _jobPositionRepository;

        public JobPositionsController(IRepository<JobPosition> jobPositionRepository)
        {
            _jobPositionRepository = jobPositionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobPosition>>> GetJobPositions()
        {
            var positions = await _jobPositionRepository.GetAllAsync();
            return Ok(positions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobPosition>> GetJobPosition(int id)
        {
            var position = await _jobPositionRepository.GetByIdAsync(id);
            if (position == null)
                return NotFound();

            return Ok(position);
        }

        [HttpPost]
        public async Task<ActionResult<JobPosition>> PostJobPosition(JobPosition jobPosition)
        {
            await _jobPositionRepository.AddAsync(jobPosition);
            await _jobPositionRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJobPosition), new { id = jobPosition.Id }, jobPosition);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobPosition(int id, JobPosition jobPosition)
        {
            if (id != jobPosition.Id)
                return BadRequest();

            var existing = await _jobPositionRepository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.Title = jobPosition.Title;
            existing.Description = jobPosition.Description;

            _jobPositionRepository.Update(existing);
            await _jobPositionRepository.SaveChangesAsync();

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobPosition(int id)
        {
            var jobPosition = await _jobPositionRepository.GetByIdAsync(id);
            if (jobPosition == null)
                return NotFound();

            _jobPositionRepository.Delete(jobPosition);
            await _jobPositionRepository.SaveChangesAsync();

            return Ok(new { message = $"Job position with ID {id} was deleted." });
        }
    }
}
