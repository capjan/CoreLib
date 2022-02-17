using System;
using System.Text.RegularExpressions;
using Core.Enums;

namespace Core.IO;

public static class PathInfo
{

    private class PathInfoData : IPathInfo
    {
        public PathInfoData(PathType type, bool isRooted, string drive, string[] parts)
        {
            Type     = type;
            Drive    = drive;
            IsRooted = isRooted;
            Parts    = parts;
        }

        public bool     IsRooted { get; }
        public PathType Type     { get; }
        public string[] Parts    { get; }
        public string   Drive    { get; }
    }

    public static IPathInfo Create(string path)
    {
        path = path ?? throw new ArgumentNullException(nameof(path));
            
        var winRootedPathMatch = Regex.Match(path, @"^(?<drive>[a-z]:)?(?<path>(\\?[^\\/]+)*)(?<trailing>\\)?$", RegexOptions.IgnoreCase);
        if (winRootedPathMatch.Success)
        {
            var drivePart = winRootedPathMatch.Groups["drive"];
            var partsGroup = winRootedPathMatch.Groups["path"];
            var pathInDrive = partsGroup.Success ? partsGroup.Value : "";
            var isRooted = pathInDrive.StartsWith("\\");
            var parts = pathInDrive.Split(new[] {'\\'}, StringSplitOptions.RemoveEmptyEntries);
            var driveLetter = drivePart.Success
                ? drivePart.Value.Substring(0,1)
                : "";
            return new PathInfoData(PathType.Windows, isRooted, driveLetter, parts);
        }

        var winPath = Regex.Match(path, @"^(?<isRooted>\\)?[^\\/]+(\\[^\\/]+)*(?<trailing>\\)?$");
        if (winPath.Success)
        {
            var parts = winPath.Value.Split(new[] {'\\'}, StringSplitOptions.RemoveEmptyEntries);
            var isRooted = path.StartsWith("\\");
            return new PathInfoData(PathType.Windows, isRooted, "", parts);
        }
            
        var nixPath = Regex.Match(path, @"^((?<isRooted>/)?([^/]+)?|\.)(/[^/]+)*(?<trailing>/)?$");
        if (nixPath.Success)
        {
            var isRooted = path.StartsWith("/");
            if (path.StartsWith("./")) path = path.Substring(1);
            var parts = path.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
               
                
            return new PathInfoData(PathType.UnixLike, isRooted, "", parts);
        }

        throw new ArgumentException("unexpected format of path", nameof(path));

    }
}