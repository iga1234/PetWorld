namespace PetWorld.Domain.Entities;

public class ChatSession
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public int IterationCount { get; set; }
    public string? SessionId { get; set; }
}
