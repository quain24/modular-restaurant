using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ModularRestaurant.Shared.Domain.Common;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ModularRestaurant.Shared.Infrastructure.EF
{
    public class DbContextBase : DbContext
    {
        private const string ConnectionString = "Sql:ConnectionString";
        private readonly IMediator _mediator;

        public DbContextBase()
        {
        }

        public DbContextBase(DbContextOptions options, IMediator mediator) : base(options)
        {
            // TODO think about creating custom date time service for events and replace DateTime.Now with custom service
            // to simplify unit testing.
            _mediator = mediator;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // TODO consider applying outbox pattern!

            var result = await base.SaveChangesAsync(cancellationToken);
            if(result > 0)
            {
                var entitiesWithEvents = ChangeTracker
                    .Entries<Entity>()
                    .Select(x => x.Entity)
                    .Where(x => x.Events.Any())
                    .ToArray();

                foreach(var entity in entitiesWithEvents)
                {
                    foreach(var domainEvent in entity.Events)
                    {
                        domainEvent.Timestamp = DateTime.Now;
                        await _mediator.Publish(domainEvent, cancellationToken);
                    }
                }
            }

            return result;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{env}.json", true)
                    .AddEnvironmentVariables()
                    .Build();

                var connectionString = configuration[ConnectionString];
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
    }
}