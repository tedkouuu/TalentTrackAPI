using Microsoft.AspNetCore.Mvc;
using TalentTrackAPI.Models;
using TalentTrackAPI.Repositories;

namespace TalentTrackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly IRepository<Candidate> _candidateRepository;

        public CandidatesController(IRepository<Candidate> candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidate>>> GetCandidates()
        {
            var candidates = await _candidateRepository.GetAllAsync();
            return Ok(candidates);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Candidate>> GetCandidate(int id)
        {
            var candidate = await _candidateRepository.GetByIdAsync(id);
            if (candidate == null)
                return NotFound();

            return Ok(candidate);
        }

        [HttpPost]
        public async Task<ActionResult<Candidate>> PostCandidate(Candidate candidate)
        {
            await _candidateRepository.AddAsync(candidate);
            await _candidateRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCandidate), new { id = candidate.Id }, candidate);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidate(int id, Candidate candidate)
        {
            if (id != candidate.Id)
                return BadRequest();

            var existing = await _candidateRepository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.FullName = candidate.FullName;
            existing.Email = candidate.Email;
            existing.Phone = candidate.Phone;

            _candidateRepository.Update(existing);
            await _candidateRepository.SaveChangesAsync();

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var candidate = await _candidateRepository.GetByIdAsync(id);
            if (candidate == null)
                return NotFound();

            _candidateRepository.Delete(candidate);
            await _candidateRepository.SaveChangesAsync();

            return Ok(new { message = $"Candidate with ID {id} was deleted." });
        }
    }
}
