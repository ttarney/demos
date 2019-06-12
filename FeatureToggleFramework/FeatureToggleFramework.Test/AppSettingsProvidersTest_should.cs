using System;
using Xunit;
using FeatureToggleFramework.Test.Support;
using FeatureToggle.Internal;
using Shouldly;
using FeatureToggleFramework.Test.Features.SimpleFeatures;
using FeatureToggle.Common.Core.Models;
using FeatureToggle.Common.Core.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FeatureToggleFramework.Test
{
    public class DbContextTest
    {
        [Fact]
        public void Foo()
        {
                FeatureFlagDbSource source = new FeatureFlagDbSource(options => options.UseSqlServer("Server=localhost\\sqlexpress;Database=FeatureFlags;Trusted_Connection=True;"));
                FeatureFlagDbProvider provider = source.Build(null) as FeatureFlagDbProvider;
                provider.Load();

                var configRoot = Helpers.BuildCustomConfig(provider.RawData);
        }
    }

    public class AppSettingsProvidersTest_should
    {
        [Fact]
        public void have_a_default_configuration()
        {
            new ASimpleFeature().FeatureEnabled.ShouldBe(true);
        }

        [Fact]
        public void enable_setting_optional_configuration()
        {
            var customConfig = Helpers.BuildCustomConfig(this.GetType());

            var appSettingsConfiguration = new AppSettingsProvider { Configuration = customConfig };

            customConfig.ShouldBeSameAs(appSettingsConfiguration.Configuration);
        }

        [Fact]
        public void use_optional_configuration_when_reading_configvalues()
        {
            var provider = new AppSettingsProvider { Configuration = Helpers.BuildCustomConfig(this.GetType()) };
            var b = new BSimpleFeature();
            b.ToggleValueProvider = provider;
            b.FeatureEnabled.ShouldBeTrue();
        }
    }
}
