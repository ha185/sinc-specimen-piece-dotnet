using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bootloader.Core.Interfaces
{
    public interface IRepository
    {
        Task<Tuple<Version, byte[]>> GetNewVersionAsync(Version version, CancellationToken cancellationToken = default(CancellationToken));
    }
}
