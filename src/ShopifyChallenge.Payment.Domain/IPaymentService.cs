using System.Threading.Tasks;
using Store.Core.DomainObjects.DTO;

namespace Store.Payment.Domain
{
    public interface IPaymentService
    {
        Task<Transaction> CheckOut(OrderPayment orderPayment);
    }
}
