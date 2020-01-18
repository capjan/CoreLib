using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Core.Security.Cryptography
{
    namespace Security
    {
        public class CRC32HashProvider : HashAlgorithm
        {
            public const uint DefaultPolynomial = 0xedb88320u;
            public const uint DefaultSeed       = 0xffffffffu;

            private static uint[] _defaultTable;

            public CRC32HashProvider()
                : this(DefaultPolynomial, DefaultSeed) { }

            public CRC32HashProvider(uint polynomial, uint seed)
            {
                if (!BitConverter.IsLittleEndian)
                    throw new PlatformNotSupportedException("Not supported on Big Endian processors");

                _table = InitializeTable(polynomial);
                _seed  = _hash = seed;
            }

            public override int HashSize => 32;

            public static uint Compute(byte[] buffer)
            {
                return Compute(DefaultSeed, buffer);
            }

            public static uint Compute(uint seed, byte[] buffer)
            {
                return Compute(DefaultPolynomial, seed, buffer);
            }

            public static uint Compute(uint polynomial, uint seed, byte[] buffer)
            {
                return ~CalculateHash(InitializeTable(polynomial), seed, buffer, 0, buffer.Length);
            }

            protected override void HashCore(byte[] array, int ibStart, int cbSize)
            {
                _hash = CalculateHash(_table, _hash, array, ibStart, cbSize);
            }

            protected override byte[] HashFinal()
            {
                var hashBuffer = UInt32ToBigEndianBytes(~_hash);
                HashValue = hashBuffer;
                return hashBuffer;
            }

            public override void Initialize()
            {
                _hash = _seed;
            }

            #region Private

            private uint _hash;

            private readonly uint   _seed;
            private readonly uint[] _table;

            private static uint CalculateHash(uint[] table, uint seed, IList<byte> buffer, int start, int size)
            {
                var hash = seed;
                for (var i = start; i < start + size; i++)
                    hash = (hash >> 8) ^ table[buffer[i] ^ (hash & 0xff)];
                return hash;
            }

            private static uint[] InitializeTable(uint polynomial)
            {
                if (polynomial == DefaultPolynomial && _defaultTable != null)
                    return _defaultTable;

                var createTable = new uint[256];
                for (var i = 0; i < 256; i++)
                {
                    var entry = (uint) i;
                    for (var j = 0; j < 8; j++)
                        if ((entry & 1) == 1)
                            entry = (entry >> 1) ^ polynomial;
                        else
                            entry = entry >> 1;
                    createTable[i] = entry;
                }

                if (polynomial == DefaultPolynomial)
                    _defaultTable = createTable;

                return createTable;
            }

            private static byte[] UInt32ToBigEndianBytes(uint uint32)
            {
                var result = BitConverter.GetBytes(uint32);

                if (BitConverter.IsLittleEndian)
                    Array.Reverse(result);

                return result;
            }

            #endregion
        }
    }
}