using Accounts.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Accounts.Persistence.AppContext;

public class AccountDbContext : DbContext
{
    public DbSet<AccountDb>? Account { get; set; }

    public AccountDbContext(DbContextOptions options) : base(options)
    {
        //Database.EnsureCreated();
    }
    
    /// <summary>
    /// All data to db
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountDb>().HasKey(e => e.Id);
        modelBuilder.Entity<AccountDb>().Property(e => e.Id).ValueGeneratedNever();
    }
}

public class CustomerContextFactory : IDesignTimeDbContextFactory<AccountDbContext>
{
    public AccountDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AccountDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Accounts;Username=postgres;Password=postgres",
            builder => builder.MigrationsAssembly(typeof(AccountDbContext).Assembly.GetName().Name));

        return new AccountDbContext(optionsBuilder.Options);
    }
}