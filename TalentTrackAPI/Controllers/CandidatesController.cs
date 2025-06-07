using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentTrackAPI.Data;
using TalentTrackAPI.Models;

namespace TalentTrackAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CandidatesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CandidatesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Candidate>>> GetCandidates()
    {
        return await _context.Candidates.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Candidate>> GetCandidate(int id)
    {
        var candidate = await _context.Candidates.FindAsync(id);

        if (candidate == null)
            return NotFound();

        return candidate;
    }

    [HttpPost]
    public async Task<ActionResult<Candidate>> PostCandidate(Candidate candidate)
    {
        _context.Candidates.Add(candidate);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCandidate), new { id = candidate.Id }, candidate);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCandidate(int id, Candidate candidate)
    {
        if (id != candidate.Id)
            return BadRequest();

        _context.Entry(candidate).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Candidates.Any(e => e.Id == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCandidate(int id)
    {
        var candidate = await _context.Candidates.FindAsync(id);
        if (candidate == null)
            return NotFound();

        _context.Candidates.Remove(candidate);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
