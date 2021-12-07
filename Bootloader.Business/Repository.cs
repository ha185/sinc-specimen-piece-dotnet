using Bootloader.Core.Interfaces;
using DataContracts;
using RestSharp;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Bootloader.Business
{
    public class Repository : IRepository
    {
        private readonly IRestClient _client;
        private readonly string _endpoint;

        public Repository(Uri baseUri, string endpoint)
        {
            this._client = new RestClient(baseUri)
            {
                Timeout = 5000
            };
            this._endpoint = endpoint;
        }

        public async Task<Tuple<Version, byte[]>> GetNewVersionAsync(Version version, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = new RestRequest(this._endpoint, Method.GET);
            request.AddParameter("version", version);

            var response = await this._client.ExecuteAsync<AppVm>(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    return new Tuple<Version, byte[]>(new Version(response.Data.Version), Convert.FromBase64String(response.Data.Base64Data));
                }
                catch (ArgumentNullException)
                {
                    // TODO: error handling and logging
                    return null;
                }
                catch (FormatException)
                {
                    // TODO: error handling and logging
                    return null;
                }
                catch
                {
                    // TODO: error handling and logging
                    throw;
                }
            }
            else if (response.StatusCode == HttpStatusCode.NoContent)
            {
                // version up-to-date
                return new Tuple<Version, byte[]>(version, null);
            }

            // TODO: error handling and logging
            return null;
        }
    }
}
