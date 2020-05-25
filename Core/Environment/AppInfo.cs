using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
