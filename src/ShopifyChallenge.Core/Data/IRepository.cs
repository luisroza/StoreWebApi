using System;
using ShopifyChallenge.Core.DomainObjects;

namespace ShopifyChallenge.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
