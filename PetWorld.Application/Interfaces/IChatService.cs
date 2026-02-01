using PetWorld.Application.DTOs;
using PetWorld.Domain.Entities;

namespace PetWorld.Application.Interfaces;

public interface IChatService
{
    Task<ChatResponseDTO> ProcessMessageAsync(ChatRequestDTO request);
    Task<IEnumerable<ChatSession>> GetHistoryAsync();
}
