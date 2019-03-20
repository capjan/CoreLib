namespace Core.IO
{
    public interface IPathNameGenerator
    {
        /// <summary>
        /// Generates a fullPath name and ensures that it's unique (that no name collusion with an existing folder or file name exists)
        /// </summary>        
        string Generate(string rootDir);
    }
}
