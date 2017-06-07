using FileDataProvider.Repositories;
using Data.Entities.Repositories;
using DbDataProvider;
using DbDataProvider.Repositories;

namespace TaskManagerASP.Tools
{
    public enum RepositoryProviderTypes
    {
        File,
        Db
    }
    public class RepositoryClient
    {
        public IRepositoryProvider GetRepositoryProvider
            (RepositoryProviderTypes repositoryType = RepositoryProviderTypes.Db)
        {
            switch (repositoryType)
            {
                case RepositoryProviderTypes.File:
                    return new FileRepositoryProvider(Configuration.GetConfig());
                case RepositoryProviderTypes.Db:
                    return new DbRepositoryProvider(new TaskManagerContextFactory().Create());
                default:
                    return new FileRepositoryProvider(Configuration.GetConfig());
            }

        }
    }
}
