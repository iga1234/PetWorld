using PetWorld.Application.Interfaces;
using PetWorld.Application.Models;
using PetWorld.Domain.Entities;
using PetWorld.Infrastructure.AI.Agents;

namespace PetWorld.Infrastructure.AI;

public class AgentOrchestrator : IAgentOrchestrator
{
    private readonly WriterAgent _writerAgent;
    private readonly CriticAgent _criticAgent;
    
    public AgentOrchestrator(WriterAgent writerAgent, CriticAgent criticAgent)
    {
        _writerAgent = writerAgent;
        _criticAgent = criticAgent;
    }  
    
    public async Task<AgentResult> ProcessAsync(string message, IEnumerable<ChatSession> history)
    {
        var iteration = 0;
        string feedback = "";
        string draft = "";
        
        for (int i = 0; i < 3; i++)
        {
            iteration++;
            draft = await _writerAgent.WriteAsync(message, history, feedback);
            var review = await _criticAgent.ReviewAsync(draft, message, history);

            if (review.Approved)
            {
                break;  
            }
            feedback = review.Feedback;
        }
        return new AgentResult
        {
            Answer = draft,
            IterationCount = iteration
        };
    }
}
