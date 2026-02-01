using System.Text.Json;
using Microsoft.SemanticKernel;

namespace PetWorld.Infrastructure.AI.Agents;

public class CriticAgent
{
    private readonly Kernel _kernel;
    
    public CriticAgent(Kernel kernel)                                                                                                                                                          
    {                                                                                                                                                                                                                                
        _kernel = kernel;                                                                                                                                                                                                            
    }   
    
    public async Task<CriticResult> ReviewAsync(string draft, string originalQuestion)                                                                                                                                           
    {                     
        var prompt = $$"""                                                                                                                                                                                                                
        You are a writing critic. Your task is to review the following response drafted by an AI assistant 
        for a customer question.
        
        Parse the response and determine if it adequately addresses the customer's question. If it does, 
        approve it. If not, provide constructive feedback on how to improve it.
        
        Respond in JSON format as follows:
        {"approved": true/false, "feedback": "your feedback here"}

        Original question: {{originalQuestion}}                                                                                                                                                                                        
        Draft response: {{draft}}     
        
        """;                                                                                                                                                                                                                         
                       
        var result = await _kernel.InvokePromptAsync(prompt);

        var json = result.ToString();
        var criticResult = JsonSerializer.Deserialize<CriticResult>(json);
        return criticResult;
    }   
}
