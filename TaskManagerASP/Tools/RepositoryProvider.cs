using FileDataProvider.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerASP.Tools
{
    public enum RepositoryProviderTypes
    {
        File
    }
    public static class RepositoryProvider
    {
        public static IRepositoryProvider GetRepositoryProvider
            (RepositoryProviderTypes repositoryType = RepositoryProviderTypes.File)
        {
            switch(repositoryType)
            {
                case RepositoryProviderTypes.File:
                    return new FileRepositoryProvider(Configuration.GetConfig());
                default:
                    return new FileRepositoryProvider(Configuration.GetConfig());
            }
            
        }
    }
}
