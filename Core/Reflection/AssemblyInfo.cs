using System;
using System.Reflection;
using Core.Extensions.ReflectionRelated;

namespace Core.Reflection
{
    public class AssemblyInfo : IAssemblyInfo
    {
        public AssemblyInfo(Assembly? assembly = default)
        {
            var usedAssembly = assembly ?? Assembly.GetEntryAssembly() ?? throw new InvalidOperationException("failed to initialize a valid assembly");
            _lazyTitle       = usedAssembly.GetAttributeResultLazy<AssemblyTitleAttribute>(a => a.Title);
            _lazyDescription = usedAssembly.GetAttributeResultLazy<AssemblyDescriptionAttribute>(a => a.Description);
            _lazyProduct = usedAssembly.GetAttributeResultLazy<AssemblyProductAttribute>(a => a.Product);
            _lazyVersion     = usedAssembly.GetAttributeResultLazy<AssemblyVersionAttribute>(a => a.Version);
            _lazyFileVersion = usedAssembly.GetAttributeResultLazy<AssemblyFileVersionAttribute>(a => a.Version);
            _lazyCompany = usedAssembly.GetAttributeResultLazy<AssemblyCompanyAttribute>(a => a.Company);
            _lazyCopyright = usedAssembly.GetAttributeResultLazy<AssemblyCopyrightAttribute>(a => a.Copyright);
            _lazyTrademark = usedAssembly.GetAttributeResultLazy<AssemblyTrademarkAttribute>(a => a.Trademark);
            _lazyInformationalVersion = usedAssembly.GetAttributeResultLazy<AssemblyInformationalVersionAttribute>(a => a.InformationalVersion);
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
