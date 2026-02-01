using System.Data;
using Microsoft.EntityFrameworkCore;
using PetWorld.Domain.Entities;

namespace PetWorld.Infrastructure.Data;

public class AppDbContext : DbContext                                                                                                                                                                                            
{                                                                                                                                                                                                                                
    public DbSet<ChatSession> ChatSessions { get; set; }                                                                                                                                                                         
    public DbSet<Product> Products { get; set; }                                                                                                                                                                                 
                                                                                                                                                                                                                                   
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)                                                                                                                                                  
    {                  
    }                                                                                                                                                                                                                            
}  
