using Microsoft.EntityFrameworkCore;
using PetWorld.Domain.Entities;
using PetWorld.Domain.IRepository;

namespace PetWorld.Infrastructure.Data.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly AppDbContext _context;
    public ChatRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<ChatSession>> GetAllAsync()
    {
        return await _context.ChatSessions.ToListAsync();
    }

    public async Task<ChatSession?> GetByIdAsync(int id)
    {
        return await _context.ChatSessions.FindAsync(id);
    }

    public async Task<IEnumerable<ChatSession>> GetBySessionIdAsync(string sessionId)
    {
        return await _context.ChatSessions.Where(x => x.SessionId == sessionId).ToListAsync();
    }

    public async Task AddAsync(ChatSession session)
    {
        await _context.ChatSessions.AddAsync(session);
        await _context.SaveChangesAsync();

    }
}
