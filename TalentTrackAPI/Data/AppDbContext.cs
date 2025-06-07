using Microsoft.EntityFrameworkCore;
using TalentTrackAPI.Models;

namespace TalentTrackAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Candidate> Candidates => Set<Candidate>();
    public DbSet<JobPosition> JobPositions => Set<JobPosition>();
    public DbSet<Application> Applications => Set<Application>();
}
