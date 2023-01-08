using System;
using System.Reflection;
using System.Security.Cryptography;
using Core.Extensions.ReflectionRelated;

namespace Core.Reflection;

public class AssemblyInfo : IAssemblyInfo
{
    /// <summary>
    /// Returns the assembly information of the assembly that contains the given type
    /// </summary>
    /// <param name="typeValue">type that is located in the assembly of interest</param>
    /// <returns>The assembly information</returns>
    public static AssemblyInfo FromType(Type typeValue)
    {
        var assembly = typeValue.Assembly;
        return new AssemblyInfo(assembly);
    }

    /// <summary>
    /// Returns the assembly information for the entry assembly.
    /// </summary>
    /// <returns>The assembly information for the entry assembly</returns>
    /// <exception cref="NullReferenceException">thrown when the resolving of the entry assembly failed</exception>
    public static AssemblyInfo FromEntryAssembly()
    {
        var assembly = Assembly.GetEntryAssembly();
        if (assembly is null)
            throw new NullReferenceException("found null where an instance for the entry assembly is expected");
        return new AssemblyInfo(assembly);
    }
    
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