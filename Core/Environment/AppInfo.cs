using System.Reflection;
using Core.Extensions.ReflectionRelated;

namespace Core.Environment
{
    public class AppInfo
    {
        public string ApplicationFolder { get; }

        public AppInfo()
        {
            ApplicationFolder = Assembly.GetEntryAssembly().GetFolderPath();
        }
    }
}
