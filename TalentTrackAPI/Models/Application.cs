namespace TalentTrackAPI.Models;

public class Application
{
    public int Id { get; set; }

    public int CandidateId { get; set; }
    public Candidate? Candidate { get; set; }

    public int JobPositionId { get; set; }
    public JobPosition? JobPosition { get; set; }

    public DateTime AppliedOn { get; set; }
}
