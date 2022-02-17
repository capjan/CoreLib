namespace Core.Reflection;

public interface IAssemblyInfo
{
    string AssemblyVersion { get; }
    string Company { get; }
    string Copyright { get; }
    string Description { get; }
    string FileVersion { get; }
    string Product { get; }
    string Title { get; }
    string Trademark { get; }
    string Version { get; }
}