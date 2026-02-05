using System.Text.Json.Serialization;

namespace PetWorld.Infrastructure.AI.Agents;

public class CriticResult                                                                                                                                                                                                        
{               
    [JsonPropertyName("approved")]
    public bool Approved { get; set; }

    [JsonPropertyName("feedback")] 
    public string Feedback { get; set; } = string.Empty;
}  
