using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OpenAI;
using PetWorld.Domain.Entities;
using PetWorld.Domain.IRepository;

namespace PetWorld.Infrastructure.AI.Agents;

public class CriticAgent
{
    private readonly string _apiKey;
    private readonly string _modelId;
    private readonly IProductRepository _productRepository;

    public CriticAgent(string apiKey, string modelId, IProductRepository productRepository)
    {
        _apiKey = apiKey;
        _modelId = modelId;
        _productRepository = productRepository;
    }

    public async Task<AIAgent> CreateAgentAsync()
    {
        var products = await _productRepository.GetAllAsync();
        var productList = string.Join("\n", products.Select(p => $"- {p.Name} (Category: {p.Category})"));

        var instructions = $"""
            You are a strict keyword-matching critic for a pet store assistant.
            Your role is to verify that the Writer's response is accurate and complete.

            ACTUAL PRODUCTS IN DATABASE:
            {productList}

            VALIDATION STEPS:
            1. EXTRACT KEYWORDS: Identify all keywords from the customer question (animal types: cat, dog, rabbit, etc. AND product types: food, toy, accessory, etc.)

            2. KEYWORD MATCHING: For each product shown in the draft, verify its Category contains ALL extracted keywords.
               - If question has "toys for dogs" → Category must contain BOTH "toy" AND "dog"
               - If question has "cats" → Category must contain "cat"
               - REJECT if any shown product doesn't match ALL keywords.

            3. COMPLETENESS CHECK: Verify ALL products from the database that match the keywords are displayed.
               - Count matching products in database.
               - Count products shown in draft.
               - REJECT if any matching product is missing.

            4. ALL PRODUCTS REQUEST:
               - If customer asks for "all products" (general) → ALL products from database must be shown.
               - If customer asks for "all" after a previous category question → only all products from that category should be shown.
               - REJECT if not all relevant products are displayed.

            5. NO HALLUCINATIONS: REJECT if any product is invented or has wrong category.

            RESPONSE FORMAT:
            If the response is CORRECT, say: "APPROVED"
            If the response is INCORRECT, say: "REJECTED: [specific reason]"
            """;

        return new OpenAIClient(_apiKey)
            .GetChatClient(_modelId)
            .AsIChatClient()
            .AsAIAgent(name: "Critic", instructions: instructions);
    }
}
