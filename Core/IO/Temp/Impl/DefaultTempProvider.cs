using System;
using System.IO;
using Core.IO.Impl;
using Core.Logging.Logger;

namespace Core.IO.Temp.Impl
{
    public class DefaultTempProvider : ITempProvider
    {
        private readonly ILogger _log = new ClassLogger<DefaultTempProvider>();

        private readonly string _rootPath = Path.GetTempPath();
        private readonly IFileNameGenerator _dirNameGen;
        private readonly IFileNameGenerator _fileNameGen;
        private readonly IFileUtil _fileUtil;
        private readonly IDirectoryUtil _directoryUtil;

        public DefaultTempProvider(            
            IFileNameGenerator dirNameGen = default, 
            IFileNameGenerator fileNameGen = default,
            IFileUtil fileUtil = default,
            IDirectoryUtil directoryUtil = default)
        {
            _directoryUtil = directoryUtil;
            var defaultNameGen = dirNameGen == null || fileNameGen == null ? new TempFileNameGenerator() : null;
            _dirNameGen = dirNameGen ?? defaultNameGen;
            _fileNameGen = fileNameGen ?? defaultNameGen;
            _fileUtil = fileUtil ?? new DefaultFileUtil();
            _directoryUtil = directoryUtil ?? new DefaultDirectoryUtil();
        }

        public string CreateDir(string parentDirectory = default)
        {
            parentDirectory = parentDirectory ?? _rootPath;
            var result = _dirNameGen.Generate(parentDirectory);
            _directoryUtil.EnsureExistence(result);
            return result;
        }

        public string CreateFile(string parentDirectory = default)
        {
            parentDirectory = parentDirectory ?? _rootPath;
            var tempFileName = _fileNameGen.Generate(parentDirectory);
            _fileUtil.Touch(tempFileName);
            return tempFileName;
        }

        public void UseDir(Action<string> action)
        {
            UseDir(_rootPath, action);
        }

        public void UseDir(string parentDirectory, Action<string> action)
        {
            var tempDirPath = CreateDir(parentDirectory);
            try
            {
                action(tempDirPath);                
            }
            catch (Exception e)
            {
                _log.Error("unexpected exception while using a temp directory", e);
            }
            finally
            {
                Directory.Delete(tempDirPath);
            }    
        }

        public void UseFile(Action<string> action)
        {
            UseFile(_rootPath, action);
        }

        public void UseFile(string parentDirectory, Action<string> action)
        {
            var tempFileName = CreateFile(parentDirectory);
            try
            {
                action(tempFileName);                
            }
            catch (Exception e)
            {
                _log.Error("unexpected exception while using a temp file", e);
            }
            finally
            {
                _fileUtil.DeleteFile(tempFileName);
            } 
        }

        /// <summary>
        /// creates a temporary directory and a temporary file in that directory and exposes them for usage in the specified lamda block
        /// </summary>
        /// <param name="action"></param>
        public void UseFile(Action<string, string> action)
        {
            UseDir(directoryPath=>UseFile(directoryPath, filePath=>action(directoryPath, filePath)));
        }
    }
}
