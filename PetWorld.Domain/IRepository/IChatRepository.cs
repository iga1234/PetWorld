using PetWorld.Domain.Entities;

namespace PetWorld.Domain.IRepository;

public interface IChatRepository
{
    Task<IEnumerable<ChatSession>> GetAllAsync();
    Task<ChatSession?> GetByIdAsync(int id);
    Task<IEnumerable<ChatSession>> GetBySessionIdAsync(string sessionId);
    Task AddAsync(ChatSession session);
}
