using System.Threading.Tasks;

namespace ShopifyChallenge.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
