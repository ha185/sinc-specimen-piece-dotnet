using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Server.Core.DomainModels;
using Server.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Storage.FileSystem.Tests
{
    [TestClass]
    public class UpdateStorageTests
    {
        private IFileRepository _repository;

        private List<Version> _versionList;

        [TestInitialize]
        public void Initialize()
        {
            var lastestVersion = new Version("5.0.0.0");

            this._versionList = new List<Version>
            {
                lastestVersion,
                new Version("2.0.0.0"),
                new Version("1.0.0.0")
            };            

            this._repository = Substitute.For<IFileRepository>();

            this._repository
                .GetVersionListAsync()
                .Returns(this._versionList);

            this._repository
                .GetAppBinaryByVersionAsync(Arg.Any<Version>())
                .Returns(Convert.FromBase64String("R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7"));

            this._repository.GetLatestVersionAsync()
                .Returns(lastestVersion);
        }

        private static IEnumerable<object[]> GetLatestVersionTests_Data
        {
            get
            {
                yield return new object[] {
                    new Version("5.0.0.0"),
                    new AppInfo {
                        Version = new Version("5.0.0.0"),
                        Data = Convert.FromBase64String("R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7")
                    }
                };

                yield return new object[] {
                    null,
                    null
                };
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(GetLatestVersionTests_Data))]
        public async Task GetLatestVersionTest(Version returnValue, AppInfo expectedResult)
        {
            // Arrange
            this._repository.GetLatestVersionAsync()
                .Returns(returnValue);

            var storage = new FileSystemUpdateStorage(this._repository);

            // Act
            var actualResult = await storage.GetLatestVersionAsync();

            // Assert
            if (expectedResult != null)
            {
                Assert.IsNotNull(actualResult);

                Assert.AreEqual(expectedResult.Version, actualResult.Version);
                Assert.AreEqual(expectedResult.Data.Length, actualResult.Data.Length);
            } 
            else
            {
                Assert.IsNull(actualResult);
            }
        }

        [TestMethod]
        public async Task GetVersionListAsyncTest()
        {
            // Arrange
            var storage = new FileSystemUpdateStorage(this._repository);

            // Act
            var list = await storage.GetVersionListAsync();

            // Assert
            Assert.AreEqual(this._versionList.Count, list.Count);
            Assert.AreEqual(typeof(List<AppBaseInfo>), list.GetType());
            Assert.AreEqual(this._versionList[0], list[0].Version);
        }

        [DataTestMethod]
        [DataRow("1.0.0.0", true)]
        [DataRow("2.9.5.0", true)]
        [DataRow("4.9.9.999", true)]
        [DataRow("5.0.0.0", false)]
        [DataRow("5.0.0.1", false)]
        public async Task IsNewVersionAvailableAsyncTest(string versionString, bool expectedResult)
        {
            // Arrange
            var version = new Version(versionString);
            var storage = new FileSystemUpdateStorage(this._repository);

            // Act
            var actualResult = await storage.IsNewVersionAvailableAsync(version);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
