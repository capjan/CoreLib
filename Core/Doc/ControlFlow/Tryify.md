# Tryify

Tyrify is a callback based utility class intended to reduce the 
required code to write TryXxx functions.

In the given use example Tyrify reduces the required lines of code from 
13 lines to 5 lines. So, with Tryify you write approx. **61.5 %** less code.

The only (and in 99% of all cases unimportant) disadvantage is that two 
additional function calls are required which could slightly reduce 
the performance.

**Calling Interface of Tyrify**
```csharp
public bool TryInvoke(Func<T> callback, out T result, T fallback = default)
```
## Example


Image you want allow the developer to work around the error handling 
and provide a TryXxxx function that returns true if everything is
fine and false in all other cases.

### Method to wrap
```csharp
// this is the method you want to wrap in a try catch block
public string Download(string url)
{
   // here is the code to download the content of the url
}
```

### Classic Approach
```csharp
public bool TryDownload(string url, out string result)
{
    Try
    {
        result = Download(url);
        return true;
    }
    catch (Exception)
    {
       result = default;
       return false;
    }
}
```
### Tryify Approach
```csharp
public bool TryDownload(string url, out string result)
{
    return new Tryify<string>()
        .TryInvoke(() => Download(url), out result);
}
```