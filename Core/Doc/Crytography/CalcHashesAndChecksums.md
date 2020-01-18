# Calculating Hashes and Checksums

Examples:

CoreLib provides string extensions to calculate hashes
```csharp
var email      = "john.doe@domain.com";

// calc hashes and checksums via string extension
var md5   = email.CalcMD5();   // E1064D48F8203C2EA2F572D1CE7EB00C
var sha1  = email.CalcSH1();   // 280F1AF70F0E726828B1BAE751306CA0131E578D
var crc32 = email.CalcCRC32(); // 657D1A11
```