using GenFu;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;


namespace FeatureToggle.Configuration
{

    public class FeatureFlagDbContext : DbContext
    {
        public FeatureFlagDbContext(DbContextOptions<FeatureFlagDbContext> options)
            : base(options)
        {

        }

        public FeatureFlagDbContext(string connectionString) : base(GetOptions(connectionString))
        {
            
        }

        public FeatureFlagDbContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var i = 1;
            var flagsToSeed = A.ListOf<FeatureFlag>(5);
            flagsToSeed.ForEach(x => x.Id = i++);
            modelBuilder.Entity<FeatureFlag>().HasData(flagsToSeed);
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        public virtual DbSet<FeatureFlag> FeatureFlags { get; set; }
    }

//public class FeatureFlagDbProvider : ConfigurationProvider
//{
//    private readonly Action<DbContextOptionsBuilder> _options;

//    public FeatureFlagDbProvider(Action<DbContextOptionsBuilder> options)
//    {
//        _options = options;
//    }

//    public override void Load()
//    {
//        var builder = new DbContextOptionsBuilder<FeatureFlagDbContext>();
//        _options(builder);

//        using (var context = new FeatureFlagDbContext(builder.Options))
//        {
//            var featureFlags = context.FeatureFlags
//                .AsNoTracking()
//                .ToList();

//            foreach (var featureFlag in featureFlags)
//            {
//                Data.Add(featureFlag.Name, featureFlag.Enabled.ToString());
//            }
//        }
//    }
//}

//public class FeatureFlagDbSource : IConfigurationSource
//{
//    private readonly Action<DbContextOptionsBuilder> _optionsAction;

//    public FeatureFlagDbSource(Action<DbContextOptionsBuilder> optionsAction)
//    {
//        _optionsAction = optionsAction;
//    }

//    public IConfigurationProvider Build(IConfigurationBuilder builder)
//    {
//        return new FeatureFlagDbProvider(_optionsAction);
//    }
//}

//public static class ConfigurationExtensions
//{
//    public static IConfigurationBuilder AddFeatureFlagDbProvider(
//        this IConfigurationBuilder configuration, Action<DbContextOptionsBuilder> setup)
//    {
//        configuration.Add(new FeatureFlagDbSource(setup));
//        return configuration;
//    }
//}

//public interface IFeatureFlagDbContext
//{
//    IDbSet<FeatureFlag> FeatureFlags { get; set; }
//}


//public class FeatureFlagDbContext : DbContext, IFeatureFlagDbContext
//{
//    public FeatureFlagDbContext(DbContextOptions<FeatureFlagDbContext> options)
//        : base(options)
//    { }

//    public DbSet<FeatureFlag> FeatureFlags { get; set; }
//}

public class FeatureFlag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Enabled{ get; set; }
    }

}
