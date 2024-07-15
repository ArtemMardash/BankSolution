using Customers.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Customers.Persistence.AppContext;

/// <summary>
/// Datagitbase context of the customer
/// </summary>
public class CustomerDbContext : DbContext
{
    /// <summary>
    /// Database set for customer
    /// </summary>
    public DbSet<CustomerDb> Customers { get; set; }
    
    public DbSet<Outbox> OutboxMessages { get; set; }
    
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
        modelBuilder.Entity<Outbox>().HasKey(e => e.Id);
        modelBuilder.Entity<Outbox>().Property(e => e.Id).ValueGeneratedNever();
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