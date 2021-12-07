using Server.Core.DomainModels;
using Server.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Storage.Database
{
    public class DatabaseUpdateStorage : IUpdateStorage
    {
        public Task<AppInfo> GetLatestVersionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<List<AppBaseInfo>> GetVersionListAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsNewVersionAvailableAsync(Version version, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
