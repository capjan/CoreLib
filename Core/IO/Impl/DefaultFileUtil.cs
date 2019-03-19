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
            _isWritable = isWritableChecker ?? new FileWritableChecker();
        }

        public void Touch(string filePath)
        {
            if (File.Exists(filePath) && !IsWritable(filePath))            
                throw new NotSupportedException("it's not possible to touch a not writable file.");             
            
            using (var fs = File.Create(filePath))            
                fs.Close();

            File.SetLastWriteTime(filePath, DateTime.Now);
        }

        public void DeleteFile(string filePath)
        {
            File.Delete(filePath);            
        }

        public bool IsWritable(string filePath)
        {
            return _isWritable.Check(filePath);
        }

        public static bool IsValidFilePath(string aFilePath)
        {

            var isValid = false;
            if (!string.IsNullOrEmpty(aFilePath))
            {
                aFilePath = aFilePath.Trim();
                if (aFilePath.Length > 0 && aFilePath.Length < 248)
                {
                    var invalidChars = Path.GetInvalidPathChars();
                    var pattern      = $"[{Regex.Escape(new string(invalidChars))}]";
                    if (!Regex.IsMatch(aFilePath, pattern))
                    {
                        isValid = true;
                    }
                }
            }

            return isValid;
        }

        private IFileWritableChecker _isWritable;

    }
}
