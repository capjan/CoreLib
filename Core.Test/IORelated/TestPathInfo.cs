using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Core.Enums;
using Core.Extensions.IORelated;
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

        [Theory]
        [InlineData(@"C:data", '\\')]
        [InlineData(@"C:\Windows", '\\')]
        [InlineData(@"d:\temp", '\\')]
        [InlineData(@"/usr/bin", '/')]
        public void TestDirSeparator(string path, char expected)
        {
            var info = PathInfo.Create(path);
            Assert.Equal(expected, info.GetDirectorySeparatorChar());
        }
        
        [Theory]
        [InlineData(@"C:data", "C:")]
        [InlineData(@"C:\Windows", @"C:\")]
        [InlineData(@"d:\temp", @"d:\")]
        [InlineData(@"/usr/bin", "/")]
        public void TestPathPath(string path, string expected)
        {
            var info = PathInfo.Create(path);
            Assert.Equal(expected, info.GetBasePath());
        }
        
        [Theory]
        [InlineData(@"C:data", new [] {"C:data"})]
        [InlineData(@"C:\Windows", new [] {@"C:\Windows"})]
        [InlineData(@"d:\temp\data.txt", new [] {@"d:\temp", @"d:\temp\data.txt"})]
        [InlineData(@"/usr/bin", new [] {"/usr", "/usr/bin"})]
        public void IterateTest(string path, string[] expected)
        {
            var info = PathInfo.Create(path);
            var actual = info.IterateParts().ToArray();
            Assert.Equal(expected, actual);
        }
    }
}
