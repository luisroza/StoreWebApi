namespace ShopifyChallenge.Payment.Domain
{
    public interface ICreditCardPaymentFacade
    {
        Transaction CheckOut(Order order, Payment payment);
    }
}
