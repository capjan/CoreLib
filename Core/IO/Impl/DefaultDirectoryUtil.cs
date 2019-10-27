using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Core.IO.Impl
{
    public class DefaultDirectoryUtil : IDirectoryUtil
    {
        private readonly char _dirSeparator = Path.DirectorySeparatorChar;
        
        public void EnsureExistence(string dirPath)
        {
            if (dirPath == null)
            {
                throw new ArgumentNullException(nameof(dirPath));
            }
            string   rootPart;
            string[] dirs;
            var      m = System.Text.RegularExpressions.Regex.Match(dirPath, "(?<drive>[a-z]:)\\\\", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (m.Success)
            {
                rootPart = m.Groups["drive"].Value;
                dirs     = dirPath.Substring(m.Value.Length).Split(_dirSeparator);
            }
            else
            {
                rootPart = Directory.GetCurrentDirectory();
                dirs     = dirPath.Split(_dirSeparator);
            }
            var currentPath = rootPart;
            foreach (var dirPart in dirs)
            {
                currentPath += _dirSeparator + dirPart;
                if (!Directory.Exists(currentPath))
                {
                    Directory.CreateDirectory(currentPath);
                }
            }
        }
    }
}
