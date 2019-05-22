# IRandom

IRandom provides a default interface to an integer random number generator. This makes it
generally possible to replace the used implementation without changes to the code.

So please prefer this interface if you need a random number generator - nothing more to say.

## Important note
The maxValue parameter is considered as an **inclusive** max value. 

So at this point, this core implementation differs from the default .NET Random implementation
where the MaxValue is considered to be exclusive.

I believe that a maximum value should always be part of the result set and therefore cause less confusion.

## Interface
```csharp
int Next();
int Next(int maxValue);
int Next(int minValue, int maxValue);
```

Every call to Next() generates a new random int number.

## Defaults
* `minValue` defaults to 0
* `maxValue` defaults to int.MaxValue (2147483647)

## Example

```ssharp