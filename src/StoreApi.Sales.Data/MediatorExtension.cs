using StoreApi.Core.Communication.Mediator;
using StoreApi.Core.DomainObjects;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApi.Sales.Data
{
    public static class MediatorExtension 
    {
        public static async Task PublishEvents(this IMediatorHandler mediator, SalesContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notifications).ToList();

            domainEntities.ToList().ForEach(e => e.Entity.ClearEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
