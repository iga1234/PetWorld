using PetWorld.Web.Components;
using PetWorld.Infrastructure;
using PetWorld.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Create database and seed data
try
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();

    var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    await initializer.Initialize();
}
catch (Exception ex)
{
    Console.WriteLine($"Database initialization failed: {ex.Message}");
    throw;
}                     

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
