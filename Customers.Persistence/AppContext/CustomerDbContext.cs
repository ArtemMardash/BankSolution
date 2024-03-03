using Customers.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Customers.Persistence.AppContext;

/// <summary>
/// Data base context of the customer
/// </summary>
public class CustomerDbContext : DbContext
{
    /// <summary>
    /// Data base set for customer
    /// </summary>
    public DbSet<CustomerDb> Customers { get; set; }
    
    public CustomerDbContext(DbContextOptions options) : base(options)
    {
        //Database.EnsureCreated();
    }

    /// <summary>
    /// All data to db
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerDb>().HasKey(e => e.Id);
        modelBuilder.Entity<CustomerDb>().Property(e => e.Id).ValueGeneratedNever();
        modelBuilder.Entity<CustomerDb>().OwnsOne<ContactsDb>("Contacts");
    }
}


public class CustomerContextFactory : IDesignTimeDbContextFactory<CustomerDbContext>
{
    public CustomerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CustomerDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Customers;Username=postgres;Password=postgres",
            builder => builder.MigrationsAssembly(typeof(CustomerDbContext).Assembly.GetName().Name));

        return new CustomerDbContext(optionsBuilder.Options);
    }
}