using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Core.Enums;
using Core.IO;

namespace Core.Extensions.IORelated
{
    public static class PathInfoExt
    {
        public static IEnumerable<string> IterateParts(this IPathInfo info)
        {
            var sb = new StringBuilder();
            var dirSeparator = info.GetDirectorySeparatorChar();
            sb.Append(info.GetBasePath(dirSeparator));
            if (info.Parts.Length == 0) yield return sb.ToString();
            else
            {
                foreach (var part in info.Parts)
                {
                    sb.Append(part);
                    yield return sb.ToString();
                    sb.Append(dirSeparator);
                }
            }
        }

        public static char GetDirectorySeparatorChar(this IPathInfo info, PathType? forcedType = null)
        {
            var usedType = forcedType ?? info.Type;
            switch (usedType)
            {
                case PathType.Windows: return '\\';
                case PathType.UnixLike: return '/';
                default: return Path.DirectorySeparatorChar;
            }
        }

        public static string GetBasePath(this IPathInfo info, char? dirSeparatorChar = default)
        {
            var sb = new StringBuilder();
            var dirChar = dirSeparatorChar ?? info.GetDirectorySeparatorChar();
            
            if (info.Type == PathType.Windows && info.Drive != null) sb.Append($"{info.Drive}:");
            if (info.IsRooted) sb.Append(dirChar);
            
            return sb.ToString();
        }
    }
}