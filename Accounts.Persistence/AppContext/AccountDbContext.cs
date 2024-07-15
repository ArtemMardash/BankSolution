using Accounts.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Accounts.Persistence.AppContext;

public class AccountDbContext : DbContext
{
    private readonly IMediator _mediator;

    public DbSet<AccountDb>? Account { get; set; }


    public AccountDbContext(DbContextOptions options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
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

    /// <summary>
    /// Save changes in db
    /// </summary>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        await DispatchDomainEvents();
        return result;
    }

    private async Task DispatchDomainEvents()
    {
        var domainEventEntities = ChangeTracker.Entries<BaseEntity>()
            .Select(entry => entry.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToList();
        var events = domainEventEntities.SelectMany(e => e.DomainEvents).ToList();
        domainEventEntities.ForEach(e=>e.DomainEvents.Clear());
        foreach (var @event in events)
        {
           await _mediator.Publish(@event);
        }
    }
}

public class CustomerContextFactory : IDesignTimeDbContextFactory<AccountDbContext>
{
    public AccountDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AccountDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Accounts;Username=postgres;Password=postgres",
            builder => builder.MigrationsAssembly(typeof(AccountDbContext).Assembly.GetName().Name));

        return new AccountDbContext(optionsBuilder.Options, null);
    }
}