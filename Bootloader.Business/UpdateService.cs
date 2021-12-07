using Bootloader.Core.DomainModels;
using Bootloader.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bootloader.Business
{
    public class UpdateService : IUpdateService
    {
        private readonly IRepository _repository;

        public UpdateService(IRepository repository)
        {
            this._repository = repository;
        }

        public async Task<AppInfo> GetNewVersionAsync(Version version, CancellationToken cancelationToken = default(CancellationToken))
        {
            var response = await this._repository.GetNewVersionAsync(version);
            if (response == null)
            {
                return null;
            }

            if (response.Item2 == null)
            {
                // version up-to-date
                return new AppInfo
                {
                    Version = response.Item1
                };
            }

            return new AppInfo
            {
                Version = response.Item1,
                Data = response.Item2
            };
        }
    }
}
