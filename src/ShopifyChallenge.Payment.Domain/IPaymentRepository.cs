using ShopifyChallenge.Core.Data;

namespace ShopifyChallenge.Payment.Domain
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        void Add(Payment payment);
        void AddTransaction(Transaction transaction);
    }
}
