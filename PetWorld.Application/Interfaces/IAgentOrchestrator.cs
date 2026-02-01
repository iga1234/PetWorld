using PetWorld.Application.Models;
using PetWorld.Domain.Entities;

namespace PetWorld.Application.Interfaces;

public interface IAgentOrchestrator
{
    Task<AgentResult> ProcessAsync(string message, IEnumerable<ChatSession> history);
}
