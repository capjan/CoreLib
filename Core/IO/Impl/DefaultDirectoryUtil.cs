using System;
using System.IO;

namespace Core.IO.Impl
{
    public class DefaultDirectoryUtil : IDirectoryUtil
    {
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
                dirs     = dirPath.Substring(m.Value.Length).Split("\\".ToCharArray());
            }
            else
            {
                rootPart = Directory.GetCurrentDirectory();
                dirs     = dirPath.Split("\\".ToCharArray());
            }
            var currentPath = rootPart;
            foreach (var dirPart in dirs)
            {
                currentPath += "\\" + dirPart;
                if (!Directory.Exists(currentPath))
                {
                    Directory.CreateDirectory(currentPath);
                }
            }
        }
    }
}
