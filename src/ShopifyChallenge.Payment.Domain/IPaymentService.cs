using System.Threading.Tasks;
using ShopifyChallenge.Core.DomainObjects.DTO;

namespace ShopifyChallenge.Payment.Domain
{
    public interface IPaymentService
    {
        Task<Transaction> CheckOut(OrderPayment orderPayment);
    }
}
