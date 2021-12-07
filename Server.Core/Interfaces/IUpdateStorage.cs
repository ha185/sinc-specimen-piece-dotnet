using Server.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Core.Interfaces
{
    public interface IUpdateStorage
    {
        Task<AppInfo> GetLatestVersionAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> IsNewVersionAvailableAsync(Version version, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<AppBaseInfo>> GetVersionListAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
