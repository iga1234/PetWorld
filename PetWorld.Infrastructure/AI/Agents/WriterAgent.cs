using Microsoft.SemanticKernel;
using PetWorld.Domain.Entities;
using PetWorld.Domain.IRepository;

namespace PetWorld.Infrastructure.AI.Agents;

public class WriterAgent
{
    private readonly Kernel _kernel;
    private readonly IProductRepository _productRepository;
    
    public WriterAgent(Kernel kernel, IProductRepository productRepository)                                                                                                                                                          
    {                                                                                                                                                                                                                                
        _kernel = kernel;                                                                                                                                                                                                            
        _productRepository = productRepository;                                                                                                                                                                                      
    }    
    
    public async Task<string> WriteAsync(string message, IEnumerable<ChatSession> history, string? criticFeedback = null) 
    {
        var products = await _productRepository.GetAllAsync();

        var productList = string.Join("\n", products.Select(p => $"- {p.Name} (Category: {p.Category}): {p.Price} zł - {p.Description}"));

        var historyText = string.Join("\n", history.Select(h => $"Customer: {h.Question}\nAssistant: {h.Answer}"));

        var prompt = $"""
            You are a helpful pet store assistant for PetWorld.

            Available products:
            {productList}

            Conversation history:
            {historyText}

            {(criticFeedback != null ? $"Your previous response was rejected. Feedback: {criticFeedback}. Please improve." : "")}

            Customer asks: {message}

            FILTERING RULES - follow strictly:
            1. ANIMAL filter (e.g. "cats", "dogs", "rabbits"): Show only products where Category contains that animal keyword.
            2. PRODUCT TYPE filter (e.g. "food", "toys", "accessories"): Show only products where Category contains that product type keyword.
            3. COMBINED filter (e.g. "toys for dogs"): Show only products where Category contains BOTH keywords.
            4. SPECIFIC PRODUCT (e.g. "Royal Canin"): Show only that exact product if it exists.
            5. ALL PRODUCTS request: Show all products regardless of category.
            6. ALL PRODUCTS for previous category (e.g. "show me all" after asking about cats): Show all products from the previously discussed category.

            STRICT RULES:
            - ONLY show products from the list above - NEVER invent products.
            - NEVER change or misrepresent product categories.
            - If no matching products exist, say so honestly.
            - Display each product on a separate line.
            - Match keywords against the Category field, not product names.
            """;                                                                                                                                                                                                                         
                       
        var result = await _kernel.InvokePromptAsync(prompt);

        return result.ToString();
    }
}
