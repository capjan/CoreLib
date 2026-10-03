using System;
using System.IO;

namespace Core.IO.Impl;

public class DefaultTempUtil : ITempUtil
{
    private readonly string _rootPath;
    private readonly IPathNameGenerator _dirNameGen;
    private readonly IPathNameGenerator _pathNameGen;
    private readonly IFileUtil _fileUtil;
    private readonly IDirectoryUtil _directoryUtil;
    private readonly bool _useBuiltInDirectoryTempCreation;
    private readonly bool _useBuiltInFileTempCreation;
    private readonly bool _createFileAtomically;

    /// <inheritdoc cref="ITempUtil.GetTempDirectory"/>
    public string GetTempDirectory()
    {
        return Path.GetTempPath();
    }

    public DefaultTempUtil(
        string? defaultRootPath = null,
        IPathNameGenerator? dirNameGen = null, 
        IPathNameGenerator? fileNameGen = null,
        IFileUtil? fileUtil = default,
        IDirectoryUtil? directoryUtil = null)
    {
        _rootPath = defaultRootPath ?? Path.GetTempPath();
        _useBuiltInDirectoryTempCreation = defaultRootPath == null && dirNameGen == null && directoryUtil == null;
        _useBuiltInFileTempCreation = defaultRootPath == null && fileNameGen == null && fileUtil == null;
        _createFileAtomically = fileUtil == null;
        _directoryUtil = directoryUtil ?? new DefaultDirectoryUtil();
        _dirNameGen = dirNameGen ?? new DefaultPathNameGenerator();;
        _pathNameGen = fileNameGen ?? new DefaultPathNameGenerator();
        _fileUtil = fileUtil ?? new DefaultFileUtil();
    }

    public string CreateDir(string? parentDirectory = default)
    {
        parentDirectory = parentDirectory ?? _rootPath;
#if NET7_0_OR_GREATER
        if (_useBuiltInDirectoryTempCreation && parentDirectory == _rootPath)
            return Directory.CreateTempSubdirectory().FullName;
#endif
        var result = _dirNameGen.Generate(parentDirectory);
        _directoryUtil.EnsureExistence(result);
        return result;
    }

    public string CreateFile(string? parentDirectory = default)
    {
        parentDirectory = parentDirectory ?? _rootPath;

        if (_useBuiltInFileTempCreation && parentDirectory == _rootPath)
            return Path.GetTempFileName();

        if (_createFileAtomically)
        {
            while (true)
            {
                var path = _pathNameGen.Generate(parentDirectory);
                try
                {
                    using var file = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                    return path;
                }
                catch (IOException) when (File.Exists(path) || Directory.Exists(path))
                {
                }
            }
        }

        var tempFileName = _pathNameGen.Generate(parentDirectory);
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
        finally
        {
            Directory.Delete(tempDirPath, recursive: true);
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
        finally
        {
            _fileUtil.Delete(tempFileName);
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
