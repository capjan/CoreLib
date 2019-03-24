# Type Checker

I wrote this class to make it easy to check if a generic type is part of a given list of types.

## use case
Imagine you want to create a generic class implementation for all numbers. 
Then you need to make sure that the generic type only allows number types.


Unfortunately, .NET lacks a simple built-in solution for this task like this:
```csharp
public class Generic<T> where T : typeof(T) in (byte, short, int, long)
{
}
``` 

So our workarount could look like this:
```csharp
public class Generic<T> where T
{
    public Generic()
    {
        var typeOfT = typeof(T);
        if (typeOfT != typeof(byte)
         && typeOfT != typeof(short)
         && typeOfT != typeof(int)
         && typeOfT != typeof(long))
            throw new ArgumentException("generic type T is not a number");        
    }
}
```

I think it's not a bad idea to make the list of allowed types configurable. So now the TypeChecker class comes in:
```csharp
public class Generic<T> where T
{
    public Generic()
    {
        var typeCheck = new TypeChecker<T>(typeof(byte), typeof(short), typeof(int), typeof(long));
        if (!typeCheck.IsValid())
            throw new ArgumentException("generic type T is not int or long");      
    }
}
```

Great, but this code looks not cleaner than the first attempt. But when we add some factory methods for the most common
type sets we make developers life easier.
```csharp
public class Generic<T> where T
{
    public Generic()
    {
        if (!TypeChecker<T>.Numeric().IsValid())
            throw new ArgumentException("generic type T is not a number");     
    }
}
```

