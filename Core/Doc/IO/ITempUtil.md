# Temporary Files and Folders

**ITempUtil** makes it easy to use temporary files and folders.

## Intended usage:

Prefer the **UseDir()** and **UseFile()** methods to create and use temporary
files/folders as intended. This methods take care to ensure that the lifetime 
of the temporary files/folders ends after the call.

Use **CreateDir()** and **CreateFile()** only if your temporary files/folders must 
life longer. If this happens: Think about changing your design, because is the time 
when disorder arises.

## Interface
```csharp
// Prefer this methods
void UseDir(Action<string> action);
void UseDir(string parentDirectory, Action<string> action);
void UseFile(Action<string> action);
void UseFile(string parentDirectory, Action<string> action);

// try to avoid this ones and use them carefully if are forced to
string CreateDir(string parentDirectory = default);
string CreateFile(string parentDirectory = default);
```

## Example

```csharp
var tmp = new DefaultTempUtil();

// create a temporary directory
tmp.UseDir(tempDirPath =>
{
    // todo: use tempDirPath
    // tempDirPath is something like "C:\Users\[username]\AppData\Local\Temp\LTKMp"
});

// create a temporary file (with size 0 bytes)
tmp.UseFile(tempFilePath =>
{
    // todo: use tempFilePath.
    // tempFilePath is something like "C:\Users\[username]\AppData\Local\Temp\x7E4U"
});
```

**custom default root directory**
```csharp
var rootDirectory = "C:\tmp";
var tmp = new DefaultTempUtil(rootDirectory);
// from now on all calls to UseDir() and UseFile() are rooted with "C:\tmp"
```

**custom file Names**
```csharp
var nameGenerator = new DefaultPathNameGenerator(postfix: ".txt");
var tmp = new DefaultTempUtil(fileNameGen: nameGenerator);
// from now on calls to UseFile() are generating file names like "au7eu.txt"
```
Take a look at the **DefaultPathNameGenerator** for the provided customiziation.
You can also implement you completly unique nameing by implementing IPathNameGenerator by your own.

## Defaults

**parentDirectory** defaults to the result of Path.GetTempPath() 

e.g. `C:\Users\[username]\AppData\Local\Temp\` in Windows 10



