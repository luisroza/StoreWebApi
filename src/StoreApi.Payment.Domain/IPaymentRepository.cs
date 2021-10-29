using StoreApi.Core.Data;

namespace StoreApi.Payment.Domain
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        void Add(Payment payment);
        void AddTransaction(Transaction transaction);
    }
}
