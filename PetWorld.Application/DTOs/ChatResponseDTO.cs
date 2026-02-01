namespace PetWorld.Application.DTOs;

public class ChatResponseDTO
{
    public string Answer { get; set; } = string.Empty;
    public int IterationCount { get; set; }
    public string SessionId { get; set; } = string.Empty;
}
