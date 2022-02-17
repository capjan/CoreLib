namespace Core.IO;

public interface IFileUtil
{
    void Delete(string filePath);
    bool IsValidFilePath(string filePath);
    bool IsWritable(string filePath);
    void Touch(string filePath);
}