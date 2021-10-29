using System.Threading.Tasks;

namespace StoreApi.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
