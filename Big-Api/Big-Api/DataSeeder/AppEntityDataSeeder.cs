using Big_Api.Data;
using Big_Api.Models;
using Bogus;

namespace Big_Api.Seeder;

public class AppEntityDataSeeder
{
  private readonly ApplicationDbContext _context;

  public AppEntityDataSeeder(ApplicationDbContext context)
  {
    _context = context;
  }

  public void Seed(int numberOfEntities)
  {
    if (!_context.appEntities.Any())
    {
      var entities = GenerateSampleData(numberOfEntities);
      _context.appEntities.AddRange(entities);
      _context.SaveChanges();
    }
  }

  private List<AppEntity> GenerateSampleData(int count)
  {
    var faker = new Faker<AppEntity>()
        .RuleFor(e => e.Name, f => f.Person.FullName);
        // Add more rules based on your properties
        

    return faker.Generate(count);
  }
}
