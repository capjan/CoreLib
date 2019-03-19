using System;
using System.IO;

namespace Core.IO.Impl
{
    public class FileWritableChecker : IFileWritableChecker
    {
        public bool Check(string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found error: {filePath}).", filePath);            

            bool result;
            try
            {
                // if the request for an exclusive file write fails, we can't write to the file.
                using (var aLockedStream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                    result = aLockedStream.CanWrite;                
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
    }
}
