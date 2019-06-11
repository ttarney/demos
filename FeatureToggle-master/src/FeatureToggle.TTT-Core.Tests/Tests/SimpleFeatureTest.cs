using System;
using Xunit;
using Shouldly;
using FeatureToggle.TTT_Core.Tests.Features.SimpleFeatures;

namespace FeatureToggle.TTT_Core.Tests
{
    public class SimpleFeatureTest_should
    {
        [Fact]
        public void be_enabled_when_default_configured()
        {
            new A().FeatureEnabled.ShouldBe(true);
        }
    }
}
