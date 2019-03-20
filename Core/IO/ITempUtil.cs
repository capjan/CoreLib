using System;

namespace Core.IO
{
    /// <summary>
    /// Makes it easy to use temporary files and folders
    /// </summary>
    public interface ITempUtil
    {        
        string CreateDir(string parentDirectory = default);
        string CreateFile(string parentDirectory = default);

        void UseDir(Action<string> action);
        void UseDir(string parentDirectory, Action<string> action);

        void UseFile(Action<string> action);
        void UseFile(string parentDirectory, Action<string> action);
    }
}
