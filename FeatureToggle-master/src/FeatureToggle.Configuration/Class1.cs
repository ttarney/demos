using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace FeatureToggle.Configuration
{
    public class DemoDbProvider : ConfigurationProvider
    {
        private readonly Action<DbContextOptionsBuilder> _options;

        public DemoDbProvider(Action<DbContextOptionsBuilder> options)
        {
            _options = options;
        }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<FeatureFlagDbContext>();
            _options(builder);

            using (var context = new FeatureFlagDbContext(builder.Options))
            {
                var featureFlags = context.FeatureFlags
                    .AsNoTracking()
                    .ToList();

                foreach (var featureFlag in featureFlags)
                {
                    Data.Add(featureFlag.Name, featureFlag.Enabled.ToString());
                }
            }
        }
    }

    public class FeatureFlagDbContext : DbContext
    {
        public FeatureFlagDbContext(DbContextOptions<FeatureFlagDbContext> options)
            : base(options)
        { }

        public DbSet<FeatureFlag> FeatureFlags { get; set; }
    }

    public class FeatureFlag
    {
        public string Name { get; set; }
        public bool Enabled{ get; set; }
    }

}
