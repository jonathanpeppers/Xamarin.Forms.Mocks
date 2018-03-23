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

        private MockFileSystemObject GetOrCreateFile(string path)
        {
            if (!_files.TryGetValue(path, out MockFileSystemObject file))
            {
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
            return Task.FromResult(_files.TryGetValue(path, out MockFileSystemObject file) && file.IsDirectory);
        }

        public Task<bool> GetFileExistsAsync(string path)
        {
            return Task.FromResult(_files.TryGetValue(path, out MockFileSystemObject file) && !file.IsDirectory);
        }

        public Task<DateTimeOffset> GetLastWriteTimeAsync(string path)
        {
            if (_files.TryGetValue(path, out MockFileSystemObject file))
            {
                return Task.FromResult(file.LastWriteTime);
            }
            throw new FileNotFoundException("Not found!", path);
        }

        public Task<Stream> OpenFileAsync(string path, FileMode mode, FileAccess access)
        {
            var file = GetOrCreateFile(path);
            file.SetModified();
            return Task.FromResult(file.Stream);
        }

        public Task<Stream> OpenFileAsync(string path, FileMode mode, FileAccess access, FileShare share)
        {
            var file = GetOrCreateFile(path);
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
