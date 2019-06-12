using FeatureToggle.Configuration;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FeatureToggle.TTT_Core.Tests.Tests
{
    public class FeatureToggleConfigurationTests
    {
        [Fact]
        public void Test()
        {
            FeatureFlagDbContext dbContext = new FeatureFlagDbContext("FeatureFlagDatabase");
            FeatureToggleConfigurationService service = new FeatureToggleConfigurationService(dbContext);
            var flags = service.AllFeatureFlags();
            foreach(FeatureFlag featureFlag in flags)
            {
                int x = 0;
            }
        }

        private void Setup()
        {
           

        }

        private IQueryable<FeatureFlag> GetFeatureFlags()
        {
            IQueryable<FeatureFlag> flags =
            new List<FeatureFlag>
            {
                new FeatureFlag { Name = "A", Enabled = true },
                new FeatureFlag { Name = "B", Enabled = true },
                new FeatureFlag { Name = "C", Enabled = true },
                new FeatureFlag { Name = "E", Enabled = true },
                new FeatureFlag { Name = "F", Enabled = true },
            }.AsQueryable();

            return flags;
        }
    }
}
