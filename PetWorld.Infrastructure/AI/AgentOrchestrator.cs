using Microsoft.Agents.AI;
using PetWorld.Application.Interfaces;
using PetWorld.Application.Models;
using PetWorld.Domain.Entities;
using PetWorld.Infrastructure.AI.Agents;

namespace PetWorld.Infrastructure.AI;

public class AgentOrchestrator : IAgentOrchestrator
{
    private readonly WriterAgent _writerAgentFactory;
    private readonly CriticAgent _criticAgentFactory;

    public AgentOrchestrator(WriterAgent writerAgentFactory, CriticAgent criticAgentFactory)
    {
        _writerAgentFactory = writerAgentFactory;
        _criticAgentFactory = criticAgentFactory;
    }

    public async Task<AgentResult> ProcessAsync(string message, IEnumerable<ChatSession> history)
    {
        var writerAgent = await _writerAgentFactory.CreateAgentAsync();
        var criticAgent = await _criticAgentFactory.CreateAgentAsync();

        var historyText = history?.Any() == true
            ? string.Join("\n", history.Select(h => $"Customer: {h.Question}\nAssistant: {h.Answer}"))
            : "";

        var fullMessage = string.IsNullOrEmpty(historyText)
            ? message
            : $"Conversation history:\n{historyText}\n\nCustomer asks: {message}";

        var iteration = 0;
        string draft = "";
        string feedback = "";

        for (int i = 0; i < 3; i++)
        {
            iteration++;

            var writerPrompt = string.IsNullOrEmpty(feedback)
                ? fullMessage
                : $"{fullMessage}\n\nYour previous response was rejected. Feedback: {feedback}. Please improve.";

            var writerResponse = await writerAgent.RunAsync(writerPrompt);
            draft = writerResponse.Text ?? string.Empty;

            var criticPrompt = $"Customer question: {message}\n\nWriter's response:\n{draft}";
            var criticResponse = await criticAgent.RunAsync(criticPrompt);
            var criticText = criticResponse.Text ?? string.Empty;

            if (criticText.Contains("APPROVED"))
            {
                break;
            }

            feedback = criticText.Replace("REJECTED:", "").Trim();
        }

        return new AgentResult
        {
            Answer = draft,
            IterationCount = iteration
        };
    }
}
