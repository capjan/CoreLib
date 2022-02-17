using System;

namespace Core.Diagnostics;

public interface ICliRunner
{
    string Arguments { get; set; }
        
    string ReadToEnd();
    void   ReadLines(Action<string> callback);
}