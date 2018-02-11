using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Mocks
{
    internal class IsolatedStorageFile : IIsolatedStorageFile
    {
        private Dictionary<string, MockFileSystemObject> _files = new Dictionary<string, MockFileSystemObject>();

        private MockFileSystemObject GetFileSystemObject(string path, bool throwOnNotFound = false)
        {
            MockFileSystemObject file;
            if (!_files.TryGetValue(path, out file))
            {
                if (throwOnNotFound)
                    throw new FileNotFoundException("Not found!", path);

                _files[path] = file = new MockFileSystemObject(false);
            }
            return file;
        }

        public Task CreateDirectoryAsync(string path)
        {
            _files[path] = new MockFileSystemObject(true);
            return Task.FromResult(true);
        }

        public Task<bool> GetDirectoryExistsAsync(string path)
        {
            var file = GetFileSystemObject(path);
            return Task.FromResult(file.IsDirectory);
        }

        public Task<bool> GetFileExistsAsync(string path)
        {
            var file = GetFileSystemObject(path);
            return Task.FromResult(!file.IsDirectory);
        }

        public Task<DateTimeOffset> GetLastWriteTimeAsync(string path)
        {
            var file = GetFileSystemObject(path);
            return Task.FromResult(file.LastWriteTime);
        }

        public Task<Stream> OpenFileAsync(string path, Internals.FileMode mode, Internals.FileAccess access)
        {
            var file = GetFileSystemObject(path);
            file.SetModified();
            return Task.FromResult(file.Stream);
        }

        public Task<Stream> OpenFileAsync(string path, Internals.FileMode mode, Internals.FileAccess access, Internals.FileShare share)
        {
            var file = GetFileSystemObject(path);
            file.SetModified();
            return Task.FromResult(file.Stream);
        }

        class MockFileSystemObject
        {
            public MockFileSystemObject(bool isDirectory)
            {
                if (!(IsDirectory = isDirectory))
                {
                    Stream = new MemoryStream();
                }
                SetModified();
            }

            public bool IsDirectory { get; private set; }

            public Stream Stream { get; private set; }

            public DateTimeOffset LastWriteTime { get; private set; }

            public void SetModified()
            {
                LastWriteTime = DateTimeOffset.Now;
            }
        }
    }
}
