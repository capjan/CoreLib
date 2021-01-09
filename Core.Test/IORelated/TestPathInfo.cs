using System.IO;
using Core.Enums;
using Core.IO;
using Core.IO.Impl;
using Xunit;

namespace Core.Test.IORelated
{
    public class TestPathInfo
    {
        [Theory]
        // [InlineData(@"C:\", true, 0, PathType.Windows)]
        // [InlineData(@"C:\Windows\System32\", true, 2, PathType.Windows)]
        // [InlineData(@"C:\Windows\System32", true, 2, PathType.Windows)]
        // [InlineData(@"\Users\username\documents\", true, 3, PathType.Windows)]
        // [InlineData(@"Users\username\documents", false, 3, PathType.Windows)]
        [InlineData(@"D:Data\data.bin", false, 2, PathType.Windows)]
        // [InlineData(@"\", true, 0, PathType.Windows)]
        // [InlineData(@"/", true, 0, PathType.UnixLike)]
        // [InlineData(@"/home/username", true, 2, PathType.UnixLike)]
        // [InlineData(@"data/readme.md", false, 2, PathType.UnixLike)]
        // [InlineData(@"./data/readme.txt", false, 2, PathType.UnixLike)]
        public void DefaultTest(string path, bool isRooted, int partCount, PathType type)
        {
            var info = PathInfo.Create(path);
            Assert.Equal(isRooted, info.IsRooted);
            Assert.Equal(partCount, info.Parts.Length);
            Assert.Equal(type, info.Type);
        }
        
    }
}
