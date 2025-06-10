using Microsoft.AspNetCore.Mvc;
using TalentTrackAPI.Models;
using TalentTrackAPI.Repositories;

namespace TalentTrackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly IRepository<Application> _applicationRepository;

        public ApplicationsController(IRepository<Application> applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> GetApplications()
        {
            var applications = await _applicationRepository.GetAllAsync();
            return Ok(applications);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> GetApplication(int id)
        {
            var application = await _applicationRepository.GetByIdAsync(id);
            if (application == null)
                return NotFound();

            return Ok(application);
        }

        [HttpPost]
        public async Task<ActionResult<Application>> PostApplication(Application application)
        {
            application.AppliedOn = DateTime.UtcNow;

            await _applicationRepository.AddAsync(application);
            await _applicationRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetApplication), new { id = application.Id }, application);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication(int id, Application application)
        {
            if (id != application.Id)
                return BadRequest();

            var existing = await _applicationRepository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.CandidateId = application.CandidateId;
            existing.JobPositionId = application.JobPositionId;
            existing.AppliedOn = application.AppliedOn;

            _applicationRepository.Update(existing);
            await _applicationRepository.SaveChangesAsync();

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            var application = await _applicationRepository.GetByIdAsync(id);
            if (application == null)
                return NotFound();

            _applicationRepository.Delete(application);
            await _applicationRepository.SaveChangesAsync();

            return Ok(new { message = $"Application with ID {id} was deleted." });
        }
    }
}
