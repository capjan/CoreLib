using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using Core.Enums;

namespace Core.IO
{
    public interface IPathInfo
    {
        bool IsRooted { get; }
        PathType Type { get; }
        
        string Drive { get; }
        string[] Parts { get; }
    }

    public partial class PathInfo : IPathInfo
    {
        public PathInfo(PathType type, bool isRooted, string drive, string[] parts)
        {
            Type = type;
            Drive = drive;
            IsRooted = isRooted;
            Parts = parts;
        }

        public bool IsRooted { get; }
        public PathType Type { get; }
        public string[] Parts { get; }
        
        public string Drive { get; }
        
        
    }
    

    partial class PathInfo: IPathInfo
    {

        public static PathInfo Create(string path)
        {
            path = path ?? throw new ArgumentNullException(nameof(path));
            
            var winRootedPathMatch = Regex.Match(path, @"^(?<drive>[a-z]:)?(?<path>(\\[^\\/]+)*)(?<trailing>\\)?$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (winRootedPathMatch.Success)
            {
                var drivePart = winRootedPathMatch.Groups["drive"];
                var partsGroup = winRootedPathMatch.Groups["path"];
                var parts = partsGroup.Success
                    ? partsGroup.Value.Split(new[] {'\\'}, StringSplitOptions.RemoveEmptyEntries)
                    : new string[] { };
                var driveLetter = drivePart.Success
                    ? drivePart.Value
                    : "";
                return new PathInfo(PathType.Windows, true, driveLetter, parts);
            }

            var winPath = Regex.Match(path, @"^(?<isRooted>\\)?[^\\/]+(\\[^\\/]+)*(?<trailing>\\)?$");
            if (winPath.Success)
            {
                var parts = winPath.Value.Split(new[] {'\\'}, StringSplitOptions.RemoveEmptyEntries);
                var isRooted = path.StartsWith("\\");
                return new PathInfo(PathType.Windows, isRooted, "", parts);
            }
            
            var nixPath = Regex.Match(path, @"^((?<isRooted>/)?([^/]+)?|\.)(/[^/]+)*(?<trailing>/)?$");
            if (nixPath.Success)
            {
                var isRooted = path.StartsWith("/");
                if (path.StartsWith("./")) path = path.Substring(1);
                var parts = path.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
               
                
                return new PathInfo(PathType.UnixLike, isRooted, "", parts);
            }

            throw new ArgumentException("unexpected format of path", nameof(path));

        }
    }
}