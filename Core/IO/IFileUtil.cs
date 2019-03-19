namespace Core.IO
{
    public interface IFileUtil
    {
        void Touch(string filePath);
        void DeleteFile(string filePath);
    }
}
