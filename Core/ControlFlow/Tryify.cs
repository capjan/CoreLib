using System;

namespace Core.ControlFlow;

public class Tryify<T>
{
    public bool TryInvoke(Func<T> callback, out T result, T fallback)
    {
        try
        {
            result = callback();
            return true;
        }
        catch (Exception)
        {
            result = fallback;
            return false;
        }
    }
}