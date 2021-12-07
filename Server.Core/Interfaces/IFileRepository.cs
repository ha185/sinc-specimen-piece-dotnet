using Server.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Core.Interfaces
{
    public interface IFileRepository
    {
        Task<List<Version>> GetVersionListAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<Version> GetLatestVersionAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<byte[]> GetAppBinaryByVersionAsync(Version version, CancellationToken cancellationToken = default(CancellationToken));
    }
}
