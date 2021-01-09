using System.IO;
using Core.IO.Impl;
using Xunit;

namespace Core.Test.IORelated
{
    public class TestTempUtil
    {
        [Fact]
        public void DefaultTempFolderExists()
        {
            var tempProvider = new DefaultTempUtil();
            var tempPath = tempProvider.GetTempDirectory();
            Assert.True(Directory.Exists(tempPath));
            
            var filenameGen = new DefaultPathNameGenerator();
            
            filenameGen.Generate(tempPath);
        }

        [Fact]
        public void TempFolderAllowsFolderCreateAndDelete()
        {
            var tempUtil = new DefaultTempUtil();
            var tempPath = tempUtil.GetTempDirectory();

            var filenameGen = new DefaultPathNameGenerator();
            var folderPath = filenameGen.Generate(tempPath);

            Assert.False(Directory.Exists(folderPath));
            Directory.CreateDirectory(folderPath);
            Assert.True(Directory.Exists(folderPath));
            Directory.Delete(folderPath);
            Assert.False(Directory.Exists(folderPath));
        }
        
        
        [Fact]
        public void TempFolderAllowsFileCreateAndDelete()
        {
            var tempUtil = new DefaultTempUtil();
            var tempPath = tempUtil.GetTempDirectory();

            var filenameGen = new DefaultPathNameGenerator();
            var filePath = filenameGen.Generate(tempPath);

            Assert.False(File.Exists(filePath));
            new DefaultFileUtil().Touch(filePath);
            Assert.True(File.Exists(filePath));
            File.Delete(filePath);
            Assert.False(File.Exists(filePath));
        }
        
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
