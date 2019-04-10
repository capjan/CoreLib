using System;
using System.Reflection;
using Core.Extensions.ReflectionRelated;

namespace Core.Reflection
{
    public class AssemblyInfo : IAssemblyInfo
    {
        public AssemblyInfo(Assembly assembly = default)
        {
            assembly = assembly ?? Assembly.GetEntryAssembly();
            _lazyTitle       = assembly.GetAttributeResultLazy<AssemblyTitleAttribute>(a => a.Title);
            _lazyDescription = assembly.GetAttributeResultLazy<AssemblyDescriptionAttribute>(a => a.Description);
            _lazyProduct = assembly.GetAttributeResultLazy<AssemblyProductAttribute>(a => a.Product);
            _lazyVersion     = assembly.GetAttributeResultLazy<AssemblyVersionAttribute>(a => a.Version);
            _lazyFileVersion = assembly.GetAttributeResultLazy<AssemblyFileVersionAttribute>(a => a.Version);
            _lazyCompany = assembly.GetAttributeResultLazy<AssemblyCompanyAttribute>(a => a.Company);
            _lazyCopyright = assembly.GetAttributeResultLazy<AssemblyCopyrightAttribute>(a => a.Copyright);
            _lazyTrademark = assembly.GetAttributeResultLazy<AssemblyTrademarkAttribute>(a => a.Trademark);
            _lazyInformationalVersion = assembly.GetAttributeResultLazy<AssemblyInformationalVersionAttribute>(a => a.InformationalVersion);
        }

        public string Title => _lazyTitle.Value;
        public string Description => _lazyDescription.Value;
        public string Product => _lazyProduct.Value;
        public string Version => _lazyInformationalVersion.Value;
        public string AssemblyVersion => _lazyVersion.Value;
        public string FileVersion => _lazyFileVersion.Value;
        public string Company => _lazyCompany.Value; 
        public string Copyright => _lazyCopyright.Value;
        public string Trademark => _lazyTrademark.Value;

        #region Private

        private readonly Lazy<string>  _lazyCompany;
        private readonly Lazy<string>  _lazyProduct;
        private readonly Lazy<string>  _lazyCopyright;
        private readonly Lazy<string>  _lazyTrademark;
        private readonly Lazy<string>  _lazyTitle;
        private readonly Lazy<string>  _lazyDescription;
        private readonly Lazy<string> _lazyVersion;
        private readonly Lazy<string> _lazyFileVersion;
        private readonly Lazy<string> _lazyInformationalVersion;

        #endregion
    }
}
