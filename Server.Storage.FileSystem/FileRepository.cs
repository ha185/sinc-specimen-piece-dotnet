using Microsoft.Extensions.Options;
using Server.Core.Interfaces;
using Server.Storage.FileSystem.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Storage.FileSystem
{
    public class FileRepository : IFileRepository
    {
        private readonly FileStorageConfiguration _configuration;

        public FileRepository(IOptions<FileStorageConfiguration> options)
        {
            this._configuration = options.Value;
        }

        public Task<byte[]> GetAppBinaryByVersionAsync(Version version, CancellationToken cancellationToken = default(CancellationToken))
        {
            return File.ReadAllBytesAsync($"{this._configuration.Path}\\{version}\\{this._configuration.ExecutableName}", cancellationToken);
        }

        public async Task<Version> GetLatestVersionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var list = await this.GetVersionListAsync(cancellationToken);

            return list.FirstOrDefault() ?? null;
        }

        public Task<List<Version>> GetVersionListAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var di = new DirectoryInfo(this._configuration.Path);

            var result = new List<Version>();

            foreach (var directory in di.GetDirectories())
            {
                result.Add(new Version(directory.Name));
            }

            return Task.FromResult(result.OrderByDescending(version => version).ToList());
        }
    }
}
