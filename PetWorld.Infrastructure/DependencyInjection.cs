using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using PetWorld.Application.Interfaces;
using PetWorld.Application.Services;
using PetWorld.Domain.IRepository;
using PetWorld.Infrastructure.AI;
using PetWorld.Infrastructure.AI.Agents;
using PetWorld.Infrastructure.Data;
using PetWorld.Infrastructure.Data.Repositories;

namespace PetWorld.Infrastructure;

public static class DependencyInjection                                                                                                                                                                                          
{                                                                                                                                                                                                                                
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)                                                                                                           
    {                                                                                                                                                                                                                            
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))));
        services.AddScoped<DbInitializer>();                                                                                                                                                                                                
          
        var kernel = Kernel.CreateBuilder()                                                                                                                                                                                              
            .AddOpenAIChatCompletion(                                                                                                                                                                                                    
                configuration["OpenAI:ModelId"],                                                                                                                                                                                         
                configuration["OpenAI:ApiKey"]                                                                                                                                                                                           
            )                                                                                                                                                                                                                            
            .Build();                                                                                                                                                                                                                    
                                                                                                                                                                                                                                   
        services.AddSingleton(kernel);

        services.AddScoped<WriterAgent>();
        services.AddScoped<CriticAgent>();
        services.AddScoped<IAgentOrchestrator, AgentOrchestrator>();
        services.AddScoped<IChatService, ChatService>();
        
        return services;                                                                                                                                                                                                         
    }                                                                                                                                                                                                                            
} 
