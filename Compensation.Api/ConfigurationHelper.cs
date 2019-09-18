using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.Api
{
    public class ConfigurationHelper
    {


        public static string GetCurrentSettings(string key)
        {

            var Builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            IConfiguration configuration = Builder.Build();

            return configuration.GetValue<string>(key);
        }

    }
}
