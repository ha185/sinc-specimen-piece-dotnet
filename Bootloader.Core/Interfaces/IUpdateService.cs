using Bootloader.Core.DomainModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bootloader.Core.Interfaces
{
    public interface IUpdateService
    {
        Task<AppInfo> GetNewVersionAsync(Version version, CancellationToken cancelationToken = default(CancellationToken));
    }
}
