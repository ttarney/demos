using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;


namespace FeatureToggleFramework.Test.Support
{
    internal  static class Helpers
    {
        public static IConfigurationRoot BuildCustomConfig(Type type)
        {
            string testDir = System.IO.Path.GetDirectoryName(type.GetTypeInfo().Assembly.Location);
            var builder = new ConfigurationBuilder().SetBasePath(testDir).AddJsonFile("customSettings.json");
            IConfigurationRoot customConfig = builder.Build();
            return customConfig;
        }

        public static IConfigurationRoot BuildCustomConfig(IDictionary<string, string> dictionaryItems)
        {
            //string testDir = System.IO.Path.GetDirectoryName(type.GetTypeInfo().Assembly.Location);
            //var builder = new ConfigurationBuilder().SetBasePath(testDir).AddJsonFile("customSettings.json");
            //IConfigurationRoot customConfig = builder.Build();
            //return customConfig;

            var builder = new ConfigurationBuilder().AddInMemoryCollection(dictionaryItems);
            IConfigurationRoot customConfig = builder.Build();
            return customConfig;
        }
    }
}
