namespace TalentTrackAPI.Models;

public class JobPosition
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<Application>? Applications { get; set; }
}
