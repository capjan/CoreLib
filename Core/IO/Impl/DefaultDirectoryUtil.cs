using System;
using System.IO;
using Core.Extensions.IORelated;

namespace Core.IO.Impl
{
    public class DefaultDirectoryUtil : IDirectoryUtil
    {
        private readonly char _dirSeparator = Path.DirectorySeparatorChar;
        
        public void EnsureExistence(string dirPath)
        {
            dirPath = dirPath ?? throw new ArgumentNullException(nameof(dirPath));
            var pathInfo = PathInfo.Create(dirPath);
            if (!pathInfo.IsRooted) throw new ArgumentException("path must be rooted", nameof(dirPath));
            
            foreach (var directoryPath in pathInfo.IterateParts())
            {
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
            }
        }
    }
}
