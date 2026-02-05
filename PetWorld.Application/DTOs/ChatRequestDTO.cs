using System.ComponentModel.DataAnnotations;

namespace PetWorld.Application.DTOs;

public class ChatRequestDTO
{
    [Required(ErrorMessage = "Message is required")]
    [StringLength(2000, ErrorMessage = "Message cannot exceed 2000 characters")]
    public string Message { get; set; } = string.Empty;

    public string? SessionId { get; set; }
}
