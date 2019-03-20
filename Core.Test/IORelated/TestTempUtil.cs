using System.IO;
using System.Threading;
using Core.IO.Impl;
using Xunit;

namespace Core.Test.IORelated
{
    public class TestTempUtil
    {
        [Fact]
        public void BasicTest()
        {
            var tmp = new DefaultTempUtil();
            var tempDirPath = "";
            var tempDirPath2 = "";
            tmp.UseDir(tempDir1 =>
            {
                tempDirPath = tempDir1;
                Assert.True(Directory.Exists(tempDir1));
                tmp.UseDir(tempDir1, tempDir2 =>
                {
                    tempDirPath2 = tempDir2;
                    Assert.True(Directory.Exists(tempDir2));
                    Assert.Equal(tempDir1, Path.GetDirectoryName(tempDirPath2));
                });
                Assert.False(Directory.Exists(tempDirPath2));
            });  
            Assert.False(Directory.Exists(tempDirPath));
        }

        [Fact]
        public void TempFilesWithCustomExtension()
        {
            var nameGenerator = new DefaultPathNameGenerator(postfix: ".txt");
            var tmp = new DefaultTempUtil(fileNameGen: nameGenerator);

            var tempFilePathCopy = "";
            tmp.UseFile(tempFilePath =>
            {
                tempFilePathCopy = tempFilePath;
                var fi = new FileInfo(tempFilePath);
                Assert.True(fi.Exists);
                Assert.Equal(0, fi.Length);
                Assert.EndsWith(".txt", tempFilePath);
            });
            Assert.False(File.Exists(tempFilePathCopy));
        }

        
    }
}
