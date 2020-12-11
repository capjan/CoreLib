# EnumUtil

Makes it easy to get information about an given Enumeration with generic syntax flavour.

**Why?**

Because I don't like the non generic API to get information about an enumeration.

Example:

```c#
// Without EnumUtil
var enumValues = Enum.GetValues(typeof(Environment.SpecialFolder));
var enumNames  = Enum.GetNames(typeof(Environment.SpecialFolder));

// let's take a look at what we got.
// 1. enumValues is of type Array! 
// WTF! Really? The non generic "Array" is totally outdated and should be removed from modern .NET at all.
// 2. Why is the determinataion of the values and the names separated in 2 function calls?
//    Hey, if I need to query the names of an enumeration, I'm pretty sure I also require the value.

// EnumUtil fixes that issues.
var info = EnumUtil.List<Environment.SpecialFolder>();

// OK! Let's take a look at what we got now.
// 1. Call accepts a generic and it also returns a generic List of Name/Value pairs
// 2. It's an IEnumerable, wo we can directly fire any Linq postprocessing
// 3. 1 call instead of 2

```

