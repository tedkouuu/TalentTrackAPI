using Microsoft.EntityFrameworkCore;
using TalentTrackAPI.Models;

namespace TalentTrackAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Candidate> Candidates => Set<Candidate>();
    public DbSet<JobPosition> JobPositions => Set<JobPosition>();
    public DbSet<Application> Applications => Set<Application>();
}
