namespace Core.Net
{
    public interface IUrlBuilder
    {
        IUrlBuilder Credentials(string userName, string password);
        IUrlBuilder Port(int? value);
        IUrlBuilder AddPath(params string[] paths);
        IUrlBuilder AddParam(string key, string value);

        IUrlBuilder ClearPath();
        IUrlBuilder ClearParameter();

        string      Build();
    }
}
