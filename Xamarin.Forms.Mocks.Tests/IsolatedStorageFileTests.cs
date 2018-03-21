using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;

namespace Xamarin.Forms.Mocks.Tests
{
    [TestFixture]
    public class IsolatedStorageFileTests
    {
        private IsolatedStorageFile _file;

        [SetUp]
        public void SetUp()
        {
            _file = new IsolatedStorageFile();
        }

        [Test]
        public async Task CreateDirectory()
        {
            string path = "/test/";
            await _file.CreateDirectoryAsync(path);
            Assert.IsTrue(await _file.GetDirectoryExistsAsync(path));
            Assert.IsFalse(await _file.GetFileExistsAsync(path));
        }

        [Test]
        public async Task OpenFile()
        {
            string path = "test.txt";
            Assert.IsNotNull(await _file.OpenFileAsync(path, 0, 0));
            Assert.IsTrue(await _file.GetFileExistsAsync(path));
            Assert.IsFalse(await _file.GetDirectoryExistsAsync(path));
        }

        [Test]
        public async Task LastWriteTime()
        {
            string path = "test.txt";
            await _file.OpenFileAsync(path, 0, 0);

            var lastWriteTime = await _file.GetLastWriteTimeAsync(path);
            await Task.Delay(10);
            await _file.OpenFileAsync(path, 0, 0);

            Assert.AreNotEqual(lastWriteTime, await _file.GetLastWriteTimeAsync(path));
        }

        [Test]
        public void LastWriteTimeOnNotFound()
        {
            Assert.ThrowsAsync<FileNotFoundException>(async () => await _file.GetLastWriteTimeAsync("asdfdasf"));
        }
    }
}
