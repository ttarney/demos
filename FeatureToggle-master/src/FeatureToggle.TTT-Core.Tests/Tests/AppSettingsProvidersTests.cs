using FeatureToggle.Internal;
using FeatureToggle.TTT_Core.Tests.Features.SimpleFeatures;
using Microsoft.Extensions.Configuration;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace FeatureToggle.TTT_Core.Tests.Tests
{
    public class AppSettingsProvidersTest_should
    {

        [Fact]
        public void have_a_default_configuration()
        {
            new A().FeatureEnabled.ShouldBe(true);
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
            var b = new B();
            b.ToggleValueProvider = provider;
            b.FeatureEnabled.ShouldBeTrue();
        }

      
    }
}
