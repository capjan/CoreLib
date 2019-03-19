namespace Core.IO.Temp
{
    public interface IFileNameGenerator
    {
        /// <summary>
        /// Generates a fullPath filename and ensures that it's unique
        /// </summary>        
        string Generate(string rootDir);
    }
}
