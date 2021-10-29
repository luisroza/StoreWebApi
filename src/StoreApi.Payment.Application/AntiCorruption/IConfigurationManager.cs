namespace StoreApi.Payment.Application.AntiCorruption
{
    public interface IConfigurationManager
    {
        string GetValue(string node);
    }
}