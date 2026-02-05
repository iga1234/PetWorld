using System.Text.Json;
using Microsoft.SemanticKernel;
using PetWorld.Domain.Entities;
using PetWorld.Domain.IRepository;

namespace PetWorld.Infrastructure.AI.Agents;

public class CriticAgent
{
    private readonly Kernel _kernel;
    private readonly IProductRepository _productRepository;

    public CriticAgent(Kernel kernel, IProductRepository productRepository)
    {
        _kernel = kernel;
        _productRepository = productRepository;
    }

    public async Task<CriticResult> ReviewAsync(string draft, string originalQuestion, IEnumerable<ChatSession> history)
    {
        var products = await _productRepository.GetAllAsync();
        var productList = string.Join("\n", products.Select(p => $"- {p.Name} (Category: {p.Category})"));
        var historyText = history?.Any() == true
            ? string.Join("\n", history.Select(h => $"Customer: {h.Question}\nAssistant: {h.Answer}"))
            : "No previous conversation";

        var prompt = $$"""
            You are a strict keyword-matching critic for a pet store assistant.

            ACTUAL PRODUCTS IN DATABASE:
            {{productList}}

            Conversation history:
            {{historyText}}

            Current customer question: {{originalQuestion}}
            Draft response to review: {{draft}}

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

            Respond ONLY in JSON format:
            {"approved": true/false, "feedback": "specific reason for rejection or approval"}
            """;

        var result = await _kernel.InvokePromptAsync(prompt);

        var json = result.ToString();
        var criticResult = JsonSerializer.Deserialize<CriticResult>(json);
        return criticResult;
    }
}
