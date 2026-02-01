using PetWorld.Domain.Entities;

namespace PetWorld.Domain.Interfaces;

public interface IChatRepository
{
    Task<IEnumerable<ChatSession>> GetAllAsync();
    Task<ChatSession?> GetByIdAsync(int id);
    Task<IEnumerable<ChatSession>> GetBySessionIdAsync(string sessionId);
    Task AddAsync(ChatSession session);
}
