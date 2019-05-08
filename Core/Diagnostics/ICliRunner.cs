using System;

namespace Core.Diagnostics
{
    public interface ICliRunner
    {
        string ReadToEnd();
        void ReadLines(Action<string> callback);
    }
}
