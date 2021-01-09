using Core.Enums;

namespace Core.IO
{
    public interface IPathInfo
    {
        bool IsRooted { get; }
        PathType Type { get; }
        string Drive { get; }
        string[] Parts { get; }
    }
}