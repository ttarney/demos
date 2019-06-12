using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FeatureToggle.Configuration
{

   
    public class FeatureToggleConfigurationService : IFeatureToggleConfigurationService
    {
        private readonly FeatureFlagDbContext _dbContext;
        public FeatureToggleConfigurationService(FeatureFlagDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<FeatureFlag> AllFeatureFlags()
        {
            return _dbContext.FeatureFlags;
                //.OrderBy(x => x.Name)
                //.ToList();
        }

        public FeatureFlag FindFeatureFlag(int id)
        {
            return _dbContext.FeatureFlags
                .FirstOrDefault(x => x.Id == id);
        }
    }

    public interface IFeatureToggleConfigurationService
    {
        IEnumerable<FeatureFlag> AllFeatureFlags();
        FeatureFlag FindFeatureFlag(int id);
    }
}

