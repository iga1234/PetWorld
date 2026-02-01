using PetWorld.Domain.Entities;

namespace PetWorld.Infrastructure.Data;

public class DbInitializer
{
    private readonly AppDbContext _context;
    
    public DbInitializer(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task Initialize()
    {
        if (!_context.Products.Any())
        {
            await _context.Products.AddAsync(new Product()
            {
                Name = "Royal Canin Adult Dog 15kg",
                Category = "Karma dla psów",
                Price = 289.00m,
                Description = "Premium karma dla dorosłych psów średnich ras"
            });

            await _context.Products.AddAsync(new Product()
            {
                Name = "Whiskas Adult Kurczak 7kg",
                Category = "Karma dla kotów",
                Price = 129.00m,
                Description = "Sucha karma dla dorosłych kotów z kurczakiem"
            });

            await _context.Products.AddAsync(new Product()
            {
                Name = "Tetra AquaSafe 500ml",
                Category = "Akwarystyka",
                Price = 45.00m,
                Description = "Uzdatniacz wody do akwarium, neutralizuje chlor"
            });

            await _context.Products.AddAsync(new Product()
            {
                Name = "Trixie Drapak XL 150cm",
                Category = "Akcesoria dla kotów",
                Price = 399.00m,
                Description = "Wysoki drapak z platformami i domkiem"
            });

            await _context.Products.AddAsync(new Product()
            {
                Name = "Kong Classic Large",
                Category = "Zabawki dla psów",
                Price = 69.00m,
                Description = "Wytrzymała zabawka do napełniania smakołykami"
            });

            await _context.Products.AddAsync(new Product()
            {
                Name = "Ferplast Klatka dla chomika",
                Category = "Gryzonie",
                Price = 189.00m,
                Description = "Klatka 60x40cm z wyposażeniem"
            });

            await _context.Products.AddAsync(new Product()
            {
                Name = "Flexi Smycz automatyczna 8m",
                Category = " Akcesoria dla psów",
                Price = 119.00m,
                Description = "Smycz zwijana dla psów do 50kg"
            });

            await _context.Products.AddAsync(new Product()
            {
                Name = "Brit Premium Kitten 8kg",
                Category = "Karma dla kotów",
                Price = 159.00m,
                Description = "Karma dla kociąt do 12 miesiąca życia"
            });

            await _context.Products.AddAsync(new Product()
            {
                Name = "JBL ProFlora CO2 Set",
                Category = "Akwarystyka",
                Price = 549.00m,
                Description = "Kompletny zestaw CO2 dla roślin akwariowych"
            });

            await _context.Products.AddAsync(new Product()
            {
                Name = "Vitapol Siano dla królików 1kg",
                Category = "Gryzonie",
                Price = 25.00m,
                Description = "Naturalne siano łąkowe, podstawa diety"
            });

            await _context.SaveChangesAsync();
        }
    }
}
