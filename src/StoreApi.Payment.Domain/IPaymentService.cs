using System.Threading.Tasks;
using StoreApi.Core.DomainObjects.DTO;

namespace StoreApi.Payment.Domain
{
    public interface IPaymentService
    {
        Task<Transaction> CheckOut(OrderPayment orderPayment);
    }
}
