using FeatureToggle.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeatureToggle.Common.Core.Configuration
{
    public class FeatureFlagDbProvider : ConfigurationProvider
    {
        private readonly Action<DbContextOptionsBuilder> _options;

        public FeatureFlagDbProvider(Action<DbContextOptionsBuilder> options)
        {
            _options = options;
        }

        public IDictionary<string, string> RawData
        {
            get
            {
                return Data;
            }
        }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<FeatureFlagsContext>();
            _options(builder);

            using (var context = new FeatureFlagsContext(builder.Options))
            {
                var items = context.FeatureFlag
                    .AsNoTracking()
                    .ToList();

                foreach (var item in items)
                {
                    Data.Add(item.Name, item.Enabled.ToString());
                }
            }
        }
    }

    public class FeatureFlagDbSource : IConfigurationSource
    {
        private readonly Action<DbContextOptionsBuilder> _optionsAction;

        public FeatureFlagDbSource(Action<DbContextOptionsBuilder> optionsAction)
        {
            _optionsAction = optionsAction;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new FeatureFlagDbProvider(_optionsAction);
        }
    }

    public static class ConfigurationExtensions
    {
        public static IConfigurationBuilder AddFeatureFlagDbProvider
            (
                this IConfigurationBuilder configuration, 
                Action<DbContextOptionsBuilder> setup
            )
        {
            configuration.Add(new FeatureFlagDbSource(setup));
            return configuration;
        }
    }
}
