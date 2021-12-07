using Bootloader.Business;
using Bootloader.Core.Interfaces;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Bootloader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var pathToApplicationExe = ConfigurationManager.AppSettings["ApplicationExecutable"];
            var versionInfo = FileVersionInfo.GetVersionInfo(pathToApplicationExe);

            Console.WriteLine($"Current product version: {versionInfo.ProductVersion}");

            var serverBaseUri = new Uri(ConfigurationManager.AppSettings["ServerBaseUri"]);
            var serverEndpoint = ConfigurationManager.AppSettings["ServerEndpoint"];

            IRepository repository = new Repository(serverBaseUri, serverEndpoint);
            IUpdateService updateService = new UpdateService(repository);

            var newAppVersion = await updateService.GetNewVersionAsync(new Version(versionInfo.ProductVersion));

            if (newAppVersion?.Data != null)
            {
                Console.WriteLine($"Updating to version {newAppVersion.Version}");
                File.WriteAllBytes(pathToApplicationExe, newAppVersion.Data);
            } 
            else
            {
                Console.WriteLine("Application up-to-date");
            }

            Thread.Sleep(2000);

            Process.Start(pathToApplicationExe);
        }
    }
}
