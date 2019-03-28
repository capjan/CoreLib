# Multi Attempt Action/Func

Reduces the necessary boiler plate code to allow multiple 
attempts for an **Action** or **Func\<T>** to one line.

## use case
Imagine a method or function with a high probability of failure. Wouldn't it be convenient 
if you could try the risky process several times and only trigger an exception 
when the number of attempts is greater than a certain threshold value?

If you could also set the wait time between attempts, it would be even better.

The class MultiAttemptAction exists exactly for this use case.

## classic solution

```csharp
Action riskyAction = () => {
   // here you super risky code comes in.
};

const int MaxAttemptCount      = 5;
var       attemptCount         = 0;
TimeSpan  delayBetweenAttempts = TimeSpan.FromSeconds(5);
var       done                 = false;

do
{
    ++attemptCount;
    try
    {        
        riskyAction.Invoke();
        done = true;
    }
    catch (Exception)
    {
        if (attemptCount > MaxAttemptCount)
            throw;                    
        Thread.Sleep(delayBetweenAttempts);
    }
} while (!done);
``` 

## solution with MultiAttemptAction
```csharp
```csharp
Action riskyAction = () => {
   // here you super risky code comes in.
};

var multiAttemptAction = new MultiAttemptAction(riskyAction, 5, TimeSpan.FromSeconds(5));
multiAttemptAction.Invoke();
```


## solution via extension method

```csharp
Action riskyAction = () => {
   // here you super risky code comes in.
};

riskyAction
    .WithMultipleAttempts(5, TimeSpan.FromSeconds(5))
    .Invoke();
``` 



