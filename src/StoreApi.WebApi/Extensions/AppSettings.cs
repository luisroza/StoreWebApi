namespace StoreApi.WebAPI.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int SessionTime { get; set; }
        public string Issuer { get; set; }
        public string ValidIn { get; set; }
    }
}
