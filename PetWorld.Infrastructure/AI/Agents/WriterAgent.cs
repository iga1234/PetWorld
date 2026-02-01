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

        var productList = string.Join("\n", products.Select(p => $"- {p.Name}: {p.Price} zł - {p.Description}"));                                                                                                                        
                                                                                                                                                                                                                                   
    var historyText = string.Join("\n", history.Select(h => $"Customer: {h.Question}\nAssistant: {h.Answer}"));                                                                                                                      
                                                                                                                                                                                                                                   
    var prompt = $"""                                                                                                                                                                                                                
        You are a helpful pet store assistant for PetWorld.                                                                                                                                                                          
                                                                                                                                                                                                                                   
        Available products:                                                                                                                                                                                                          
        {productList}                                                                                                                                                                                                                
                                                                                                                                                                                                                                   
        Conversation history:                                                                                                                                                                                                        
        {historyText}                                                                                                                                                                                                                
                                                                                                                                                                                                                                   
        {(criticFeedback != null ? $"Your previous response was rejected. Feedback: {criticFeedback}. Please improve." : "")}                                                                                                        
                                                                                                                                                                                                                                   
        Customer asks: {message}                                                                                                                                                                                                     
                                                                                                                                                                                                                                   
        Provide a helpful response and recommend relevant products from the catalog.                                                                                                                                                 
        """;                                                                                                                                                                                                                         
                       
        var result = await _kernel.InvokePromptAsync(prompt);

        return result.ToString();
    }
}
