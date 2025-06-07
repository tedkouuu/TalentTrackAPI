﻿namespace TalentTrackAPI.Models;

public class Candidate
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;

    public ICollection<Application>? Applications { get; set; }
}
