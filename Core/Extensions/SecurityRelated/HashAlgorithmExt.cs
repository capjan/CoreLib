using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using Core.Enums;
using Core.Extensions.TextRelated;
using Core.Security.Cryptography.Security;

namespace Core.Extensions.SecurityRelated;

public static class HashAlgorithmExt
{

    public static HashAlgorithm CreateAlgorithm(this HashType hashType)
    {
        switch (hashType)
        {
            case HashType.CRC32: return new CRC32HashProvider();
            case HashType.MD5: return MD5.Create();
            case HashType.SHA1: return SHA1.Create();
            default: throw new ArgumentException($"unsupported hash type {hashType}");
        }
    }

    public static byte[] CalcChecksum<T>(this IEnumerable<byte> bytes) where T : HashAlgorithm, new()
    {
        var data = bytes as byte[] ?? bytes.ToArray();
        using (var hashAlgorithm = new T())
            return hashAlgorithm.ComputeHash(data);
    }

    public static byte[] CalcChecksum<T>(this Stream stream) where T : HashAlgorithm, new()
    {
        using (var hashAlgorithm = new T())
            return hashAlgorithm.ComputeHash(stream);
    }

    public static byte[] CalcChecksum<T>(this string value) where T : HashAlgorithm, new()
    {
        using (var stream = value.ToMemoryStream())
            return stream.CalcChecksum<T>();
    }

    public static string Checksum(this IEnumerable<byte> value, HashType hashType)
    {
        var data = value as byte[] ?? value.ToArray();
        using (var hashAlgorithm = hashType.CreateAlgorithm())
            return hashAlgorithm.ComputeHash(data).ToHexString();
    }

    public static string Checksum(this Stream stream, HashType hashType)
    {
        using (var hashAlgorithm = hashType.CreateAlgorithm())
            return hashAlgorithm.ComputeHash(stream).ToHexString();
    }

    public static string Checksum(this string value, HashType hashType)
    {
        using (var stream = value.ToMemoryStream())
            return stream.Checksum(hashType);
    }

    public static string CalcCRC32(this string value)
    {
        return value.CalcChecksum<CRC32HashProvider>().ToHexString();
    }

    public static string CalcMD5(this string value)
    {
        return MD5.Create().ComputeChecksum(value);
    }

    public static string CalcSHA1(this string value)
    {
        return SHA1.Create().ComputeChecksum(value);
    }


    public static string ComputeChecksum(this HashAlgorithm algorithm, string input)
    {
        using var ms = new MemoryStream();
        var inputBytes = Encoding.UTF8.GetBytes(input);
        ms.Write(inputBytes, 0, inputBytes.Length);
        ms.Seek(0, SeekOrigin.Begin);
        return algorithm.ComputeChecksum(ms);
    }

    public static string ComputeChecksum(this HashAlgorithm algorithm, Stream inputStream)
    {
        var hashData = algorithm.ComputeHash(inputStream);
        return hashData.ToHexString();
    }
}
