# CompositeReadOnlyList

Exposes two given readonly lists as one continous list.

## Note
The IReadOnlyList\<T\> represents a list in which the number and order of list elements is read-only. The content of list elements is not guaranteed to be read-only.

## Example


```csharp
var readonly1 = new ReadOnlyCollection<int>(new int[] {1, 2, 3});
var readonly2 = new ReadOnlyCollection<int>(new int[] {4, 5, 6});

var compositeList = new CompositeReadOnlyList<int>(readonly1, readonly2);
``` 



