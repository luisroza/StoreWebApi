namespace ShopifyChallenge.Payment.Application.AntiCorruption
{
    public interface IConfigurationManager
    {
        string GetValue(string node);
    }
}