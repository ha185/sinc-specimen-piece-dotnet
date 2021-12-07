using Server.Core.DomainModels;
using Server.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Storage.FileSystem
{
    public class FileSystemUpdateStorage : IUpdateStorage
    {
        private readonly IFileRepository _repository;

        public FileSystemUpdateStorage(IFileRepository repository)
        {
            this._repository = repository;
        }

        public async Task<AppInfo> GetLatestVersionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var latestVersion = await this._repository.GetLatestVersionAsync(cancellationToken);
            if (latestVersion == null)
            {
                return null;
            }

            return new AppInfo
            {
                Version = latestVersion,
                Data = await this._repository.GetAppBinaryByVersionAsync(latestVersion, cancellationToken)
            };
        }

        public async Task<List<AppBaseInfo>> GetVersionListAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var list = await this._repository.GetVersionListAsync(cancellationToken);

            return list.Select(version => new AppBaseInfo { Version = version }).ToList();
        }

        public async Task<bool> IsNewVersionAvailableAsync(Version version, CancellationToken cancellationToken = default(CancellationToken))
        {
            var latestVersion = await this._repository.GetLatestVersionAsync(cancellationToken);
            if (latestVersion == null || latestVersion <= version)
            {
                return false;
            }

            return true;
        }
    }
}
