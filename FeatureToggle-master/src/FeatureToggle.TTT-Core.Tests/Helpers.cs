using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FeatureToggle.TTT_Core.Tests
{
    internal static class Helpers
    {
        public static IConfigurationRoot BuildCustomConfig(Type type)
        {
            string testDir = System.IO.Path.GetDirectoryName(type.GetTypeInfo().Assembly.Location);
            var builder = new ConfigurationBuilder().SetBasePath(testDir).AddJsonFile("customSettings.json");
            IConfigurationRoot customConfig = builder.Build();
            return customConfig;
        }
    }
}
