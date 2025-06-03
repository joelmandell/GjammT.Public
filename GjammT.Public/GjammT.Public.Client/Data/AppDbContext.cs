using GjammT.Public.Client.Models;

namespace GjammT.Public.Client.Data;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    private readonly string _connectionString = "AccountEndpoint=https://gjammt-cosmos.documents.azure.com:443/;AccountKey=A9jORh2mqIl2VJxjqJzXN3Dk7YCUWzIUaOqs6UCunBJ2IggHAMZuifIC50ZVG8REbpRivxNZaLcBACDbYtOSHQ==;";
    private readonly string _databaseName = "web"; // 👈 Replace with your Database Name

    public DbSet<Page> Pages { get; set; }

    public AppDbContext()
    {
    }

    // Optional: If you are using ASP.NET Core's dependency injection
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) //
        {
            optionsBuilder.UseCosmos(
               "https://gjammt-cosmos.documents.azure.com:443/",
                "A9jORh2mqIl2VJxjqJzXN3Dk7YCUWzIUaOqs6UCunBJ2IggHAMZuifIC50ZVG8REbpRivxNZaLcBACDbYtOSHQ==",
               databaseName:"web"
            );
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Page>()
            .ToContainer("Page")
            .HasPartitionKey(i => i.PartitionKey); // 👈 Configure the partition key property

        // You can also specify the default container for all entity types if not specified individually
        // modelBuilder.HasDefaultContainer("DefaultContainerName");

        // If your 'id' property is named differently, e.g., 'ItemId'
        // modelBuilder.Entity<Item>()
        //    .HasKey(i => i.ItemId); // This maps ItemId to the 'id' property in Cosmos DB
        //    .Property(i => i.ItemId)
        //    .ToJsonProperty("id"); // Explicitly map if your C# property name isn't 'id'

        // Ensure no discriminator is used if you only have one type in the container
        // or if you manage types differently.
        modelBuilder.Entity<Page>().HasNoDiscriminator();
    }
}