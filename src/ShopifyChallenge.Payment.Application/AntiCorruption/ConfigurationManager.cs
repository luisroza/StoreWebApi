using System;
using System.Linq;

namespace Store.Payment.Application.AntiCorruption
{
    public class ConfigurationManager : IConfigurationManager
    {
        //Get this information from a config file - simulation purpose
        public string GetValue(string node)
        {
            return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
}
