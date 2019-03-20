using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Core.IO.Impl
{
    public class DefaultFileUtil : IFileUtil
    {
        public DefaultFileUtil(
            IFileWritableChecker isWritableChecker = default)
        {
            _isWritableChecker = isWritableChecker ?? new FileWritableChecker();
        }

        public void Touch(string filePath)
        {
            if (File.Exists(filePath) && !IsWritable(filePath))            
                throw new NotSupportedException("it's not possible to touch a not writable file.");             
            
            using (var fs = File.Create(filePath))            
                fs.Close();

            File.SetLastWriteTime(filePath, DateTime.Now);
        }

        public void Delete(string filePath)
        {
            File.Delete(filePath);            
        }

        public bool IsWritable(string filePath)
        {
            return _isWritableChecker.Check(filePath);
        }

        public bool IsValidFilePath(string filePath)
        {
            var isValid = false;
            if (!string.IsNullOrEmpty(filePath))
            {
                filePath = filePath.Trim();
                if (filePath.Length > 0 && filePath.Length < 248)
                {
                    var invalidChars = Path.GetInvalidPathChars();
                    var pattern      = $"[{Regex.Escape(new string(invalidChars))}]";
                    if (!Regex.IsMatch(filePath, pattern))
                    {
                        isValid = true;
                    }
                }
            }
            return isValid;
        }

        private readonly IFileWritableChecker _isWritableChecker;

    }
}
