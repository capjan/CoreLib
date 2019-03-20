# IRandom

IRandom provides a default interface to an integer random number generator. This makes it
generally possible to replace the used implementation without changes to the code.

So please prefer this interface if you need a random number generator - nothing more to say.

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