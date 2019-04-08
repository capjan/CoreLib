using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Core.Enums;
using Core.Extensions.TextRelated;
using Core.Security.Cryptography.Security;

namespace Core.Extensions.SecurityRelated
{
    public static class HashAlgorithmExt
    {

        public static HashAlgorithm CreateAlgorithm(this HashType hashType)
        {
            switch (hashType)
            {
                case HashType.CRC32: return new Crc32HashProvider();
                case HashType.MD5: return new MD5CryptoServiceProvider();
                case HashType.SHA1: return new SHA1CryptoServiceProvider();
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

        public static string CalcCrc32(this string value)
        {
            return value.CalcChecksum<Crc32HashProvider>().ToHexString();
        }

        public static string CalcMd5(this string value)
        {
            return value.CalcChecksum<MD5CryptoServiceProvider>().ToHexString();
        }

        public static string CalcSha1(this string value)
        {
            return value.CalcChecksum<SHA1CryptoServiceProvider>().ToHexString();
        }

    }
}
