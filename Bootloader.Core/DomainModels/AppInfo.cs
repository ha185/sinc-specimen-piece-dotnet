using System;

namespace Bootloader.Core.DomainModels
{
    public class AppInfo
    {
        public Version Version { get; set; }

        public byte[] Data { get; set; }
    }
}
