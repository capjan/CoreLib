# Calculating Hashes and Checksums

Examples:

Calculate hashes and checksums via String extension
```csharp
var email      = "john.doe@domain.com";

// calc hashes and checksums via string extension
var md5   = email.CalcMD5();
var sha1  = email.CalcSH1();
var crc32 = email.CalcCRC32();
```