using System;

namespace SHA1_Collisions
{
    class CustomSHA1
    {
        private UInt32[] digest = new UInt32[5];
        private UInt32[] buffer = new UInt32[80];

        private void Reset()
        {
            // SHA1 initialization constants 
            digest[0] = 0x67452301;
            digest[1] = 0xefcdab89;
            digest[2] = 0x98badcfe;
            digest[3] = 0x10325476;
            digest[4] = 0xc3d2e1f0;
        }

        private void ClearBuffer()
        {
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = 0;
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UInt32 Rol(UInt32 value, Int32 bits)
        {
            return (value << bits) | (value >> (32 - bits));
        }

        private static UInt32 Blk(UInt32[] block, Int32 i)
        {
            return Rol(block[(i + 13) & 15] ^ block[(i + 8) & 15] ^ block[(i + 2) & 15] ^ block[i], 1);
        }

        public CustomSHA1()
        {
            Reset();
        }

        private void InnerHash()
        {
            UInt32 a = digest[0];
            UInt32 b = digest[1];
            UInt32 c = digest[2];
            UInt32 d = digest[3];
            UInt32 e = digest[4];

            int round = 0;

            while (round < 16)
            {
                uint t = Rol(a, 5) + ((b & c) | (~b & d)) + e + 0x5a827999 + buffer[round];
                e = d; 
				d = c; 
				c = Rol(b, 30); 
				b = a; 
				a = t;
                ++round;
            }
            while (round < 20)
            {
                buffer[round] = Rol(buffer[round - 3] ^ buffer[round - 8] ^ buffer[round - 14] ^ buffer[round - 16], 1);
                uint t = Rol(a, 5) + ((b & c) | (~b & d)) + e + 0x5a827999 + buffer[round];
                e = d;
                d = c;
                c = Rol(b, 30);
                b = a;
                a = t;
                ++round;
            }
            while (round < 40)
            {
                buffer[round] = Rol(buffer[round - 3] ^ buffer[round - 8] ^ buffer[round - 14] ^ buffer[round - 16], 1);
                uint t = Rol(a, 5) + (b ^ c ^ d) + e + 0x6ed9eba1 + buffer[round];
                e = d;
                d = c;
                c = Rol(b, 30);
                b = a;
                a = t;
                ++round;
            }
            while (round < 60)
            {
                buffer[round] = Rol(buffer[round - 3] ^ buffer[round - 8] ^ buffer[round - 14] ^ buffer[round - 16], 1);
                uint t = Rol(a, 5) + ((b & c) | (b & d) | (c & d)) + e + 0x8f1bbcdc + buffer[round];
                e = d;
                d = c;
                c = Rol(b, 30);
                b = a;
                a = t;
                ++round;
            }
            while (round < 80)
            {
                buffer[round] = Rol(buffer[round - 3] ^ buffer[round - 8] ^ buffer[round - 14] ^ buffer[round - 16], 1);
                uint t = Rol(a, 5) + (b ^ c ^ d) + e + 0xca62c1d6 + buffer[round];
                e = d;
                d = c;
                c = Rol(b, 30);
                b = a;
                a = t;
                ++round;
            }

            digest[0] += a;
            digest[1] += b;
            digest[2] += c;
            digest[3] += d;
            digest[4] += e;
        }

        public void ComputeHash(Byte[] source, Byte[] hash)
        {
            Reset();
            ClearBuffer();

            int byteLength = source.Length;
            int endOfFullBlocks = byteLength - 64;
            int endCurrentBlock;
            int currentBlock = 0;

            while (currentBlock <= endOfFullBlocks)
            {
                endCurrentBlock = currentBlock + 64;

                // Init the round buffer with the 64 byte block data.
                for (int roundPos = 0; currentBlock < endCurrentBlock; currentBlock += 4)
                {
                    // This line will swap endian on big endian and keep endian on little endian.
                    buffer[roundPos++] = (uint)source[currentBlock + 3]
                        | (((uint)source[currentBlock + 2]) << 8)
                        | (((uint)source[currentBlock + 1]) << 16)
                        | (((uint)source[currentBlock]) << 24);
                }
                InnerHash();
            }

            // Handle the last and not full 64 byte block if existing.
            endCurrentBlock = byteLength - currentBlock;
            ClearBuffer();
            int lastBlockBytes = 0;
            for (; lastBlockBytes < endCurrentBlock; ++lastBlockBytes)
            {
                buffer[lastBlockBytes >> 2] |= (uint)source[lastBlockBytes + currentBlock] << ((3 - (lastBlockBytes & 3)) << 3);
            }

            buffer[lastBlockBytes >> 2] |= (uint)0x80 << ((3 - (lastBlockBytes & 3)) << 3);

            if (endCurrentBlock >= 56)
            {
                InnerHash();
                ClearBuffer();
            }

            buffer[15] = (uint)byteLength << 3;
            InnerHash();

            for (int hashByte = 20; --hashByte >= 0;)
            {
                hash[hashByte] = (byte)((digest[hashByte >> 2] >> (((3 - hashByte) & 0x3) << 3)) & 0xff);
            }
        }

        ///*
        // * (R0+R1), R2, R3, R4 are the different operations used in SHA1
        // */
        //private static void R0(UInt32[] block, UInt32 v, ref UInt32 w, UInt32 x, UInt32 y, ref UInt32 z, Int32 i)
        //{
        //    z += ((w & (x ^ y)) ^ y) + block[i] + 0x5a827999 + Rol(v, 5);
        //    w = Rol(w, 30);
        //}

        //private static void R1(UInt32[] block, UInt32 v, ref UInt32 w, UInt32 x, UInt32 y, ref UInt32 z, Int32 i)
        //{
        //    block[i] = Blk(block, i);
        //    z += ((w & (x ^ y)) ^ y) + block[i] + 0x5a827999 + Rol(v, 5);
        //    w = Rol(w, 30);
        //}

        //private static void R2(UInt32[] block, UInt32 v, ref UInt32 w, UInt32 x, UInt32 y, ref UInt32 z, Int32 i)
        //{
        //    block[i] = Blk(block, i);
        //    z += (w ^ x ^ y) + block[i] + 0x6ed9eba1 + Rol(v, 5);
        //    w = Rol(w, 30);
        //}

        //private static void R3(UInt32[] block, UInt32 v, ref UInt32 w, UInt32 x, UInt32 y, ref UInt32 z, Int32 i)
        //{
        //    block[i] = Blk(block, i);
        //    z += (((w | x) & y) | (w & x)) + block[i] + 0x8f1bbcdc + Rol(v, 5);
        //    w = Rol(w, 30);
        //}

        //private static void R4(UInt32[] block, UInt32 v, ref UInt32 w, UInt32 x, UInt32 y, ref UInt32 z, Int32 i)
        //{
        //    block[i] = Blk(block, i);
        //    z += (w ^ x ^ y) + block[i] + 0xca62c1d6 + Rol(v, 5);
        //    w = Rol(w, 30);
        //}

        ///*
        // * Hash a single 512-bit block. This is the core of the algorithm.
        // */
        //private void Transform(UInt32[] block)
        //{
        //    UInt32 a = digest[0];
        //    UInt32 b = digest[1];
        //    UInt32 c = digest[2];
        //    UInt32 d = digest[3];
        //    UInt32 e = digest[4];

        //    R0(block, a, ref b, c, d, ref e, 0);
        //    R0(block, e, ref a, b, c, ref d, 1);
        //    R0(block, d, ref e, a, b, ref c, 2);
        //    R0(block, c, ref d, e, a, ref b, 3);
        //    R0(block, b, ref c, d, e, ref a, 4);
        //    R0(block, a, ref b, c, d, ref e, 5);
        //    R0(block, e, ref a, b, c, ref d, 6);
        //    R0(block, d, ref e, a, b, ref c, 7);
        //    R0(block, c, ref d, e, a, ref b, 8);
        //    R0(block, b, ref c, d, e, ref a, 9);
        //    R0(block, a, ref b, c, d, ref e, 10);
        //    R0(block, e, ref a, b, c, ref d, 11);
        //    R0(block, d, ref e, a, b, ref c, 12);
        //    R0(block, c, ref d, e, a, ref b, 13);
        //    R0(block, b, ref c, d, e, ref a, 14);
        //    R0(block, a, ref b, c, d, ref e, 15);
        //    R1(block, e, ref a, b, c, ref d, 0);
        //    R1(block, d, ref e, a, b, ref c, 1);
        //    R1(block, c, ref d, e, a, ref b, 2);
        //    R1(block, b, ref c, d, e, ref a, 3);
        //    R2(block, a, ref b, c, d, ref e, 4);
        //    R2(block, e, ref a, b, c, ref d, 5);
        //    R2(block, d, ref e, a, b, ref c, 6);
        //    R2(block, c, ref d, e, a, ref b, 7);
        //    R2(block, b, ref c, d, e, ref a, 8);
        //    R2(block, a, ref b, c, d, ref e, 9);
        //    R2(block, e, ref a, b, c, ref d, 10);
        //    R2(block, d, ref e, a, b, ref c, 11);
        //    R2(block, c, ref d, e, a, ref b, 12);
        //    R2(block, b, ref c, d, e, ref a, 13);
        //    R2(block, a, ref b, c, d, ref e, 14);
        //    R2(block, e, ref a, b, c, ref d, 15);
        //    R2(block, d, ref e, a, b, ref c, 0);
        //    R2(block, c, ref d, e, a, ref b, 1);
        //    R2(block, b, ref c, d, e, ref a, 2);
        //    R2(block, a, ref b, c, d, ref e, 3);
        //    R2(block, e, ref a, b, c, ref d, 4);
        //    R2(block, d, ref e, a, b, ref c, 5);
        //    R2(block, c, ref d, e, a, ref b, 6);
        //    R2(block, b, ref c, d, e, ref a, 7);
        //    R3(block, a, ref b, c, d, ref e, 8);
        //    R3(block, e, ref a, b, c, ref d, 9);
        //    R3(block, d, ref e, a, b, ref c, 10);
        //    R3(block, c, ref d, e, a, ref b, 11);
        //    R3(block, b, ref c, d, e, ref a, 12);
        //    R3(block, a, ref b, c, d, ref e, 13);
        //    R3(block, e, ref a, b, c, ref d, 14);
        //    R3(block, d, ref e, a, b, ref c, 15);
        //    R3(block, c, ref d, e, a, ref b, 0);
        //    R3(block, b, ref c, d, e, ref a, 1);
        //    R3(block, a, ref b, c, d, ref e, 2);
        //    R3(block, e, ref a, b, c, ref d, 3);
        //    R3(block, d, ref e, a, b, ref c, 4);
        //    R3(block, c, ref d, e, a, ref b, 5);
        //    R3(block, b, ref c, d, e, ref a, 6);
        //    R3(block, a, ref b, c, d, ref e, 7);
        //    R3(block, e, ref a, b, c, ref d, 8);
        //    R3(block, d, ref e, a, b, ref c, 9);
        //    R3(block, c, ref d, e, a, ref b, 10);
        //    R3(block, b, ref c, d, e, ref a, 11);
        //    R4(block, a, ref b, c, d, ref e, 12);
        //    R4(block, e, ref a, b, c, ref d, 13);
        //    R4(block, d, ref e, a, b, ref c, 14);
        //    R4(block, c, ref d, e, a, ref b, 15);
        //    R4(block, b, ref c, d, e, ref a, 0);
        //    R4(block, a, ref b, c, d, ref e, 1);
        //    R4(block, e, ref a, b, c, ref d, 2);
        //    R4(block, d, ref e, a, b, ref c, 3);
        //    R4(block, c, ref d, e, a, ref b, 4);
        //    R4(block, b, ref c, d, e, ref a, 5);
        //    R4(block, a, ref b, c, d, ref e, 6);
        //    R4(block, e, ref a, b, c, ref d, 7);
        //    R4(block, d, ref e, a, b, ref c, 8);
        //    R4(block, c, ref d, e, a, ref b, 9);
        //    R4(block, b, ref c, d, e, ref a, 10);
        //    R4(block, a, ref b, c, d, ref e, 11);
        //    R4(block, e, ref a, b, c, ref d, 12);
        //    R4(block, d, ref e, a, b, ref c, 13);
        //    R4(block, c, ref d, e, a, ref b, 14);
        //    R4(block, b, ref c, d, e, ref a, 15);

        //    Console.WriteLine("ABCDE");
        //    Console.WriteLine($"{a}\n{b}\n{c}\n{d}\n{e}");

        //    /* Add the working vars back into digest[] */
        //    digest[0] += a;
        //    digest[1] += b;
        //    digest[2] += c;
        //    digest[3] += d;
        //    digest[4] += e;

        //    transforms++;
        //}

        ///* 
        // * Convert Byte buffer to a UInt32 array (MSB) 
        // */
        //private void BufferToBlock(Byte[] buffer, UInt32[] block)
        //{
        //    for (uint i = 0; i < BLOCK_INTS; i++)
        //    {
        //        block[i] = (buffer[4 * i + 3] & (uint)0xff)
        //           | (buffer[4 * i + 2] & (uint)0xff) << 8
        //           | (buffer[4 * i + 1] & (uint)0xff) << 16
        //           | (buffer[4 * i + 0] & (uint)0xff) << 24;
        //    }
        //}

        /*public void ComputeHash(Byte[] source, Byte[] hash)
        {
            Reset();

            int bufferLength = source.Length;
            int endOfFullBlocks = bufferLength - 64;
            int endOfCurrentBlock;
            int currentBlock = 0;

            while (currentBlock <= endOfFullBlocks)
            {
                endOfCurrentBlock = currentBlock + 64;

                // Init the round buffer with the 64 byte block data.
                for (uint i = 0; i < BLOCK_INTS; i++)
                {
                    buffer[i] = (source[4 * i + 3] & (uint)0xff)
                       | (source[4 * i + 2] & (uint)0xff) << 8
                       | (source[4 * i + 1] & (uint)0xff) << 16
                       | (source[4 * i + 0] & (uint)0xff) << 24;
                }

                Transform(buffer);
            }

            // Handle the last and not full 64 byte block if existing.
            endOfCurrentBlock = bufferLength - currentBlock;
            ClearBuffer();
            short lastBlockBytes = 0;
            for (; lastBlockBytes < endOfCurrentBlock; ++lastBlockBytes)
            {
                buffer[lastBlockBytes >> 2] |= (uint)source[lastBlockBytes + currentBlock] << ((3 - (lastBlockBytes & 3)) << 3);
            }

            buffer[lastBlockBytes >> 2] |= (ushort)(0x80 << ((3 - (lastBlockBytes & 3)) << 3));
            if (endOfCurrentBlock >= 56)
            {
                Transform(buffer);
                ClearBuffer();
            }
            buffer[15] = (uint)bufferLength << 3;

            for (int i = 0; i < 5; i++)
                Console.Write($"{digest[i]} ");
            for (int i = 0; i < 5; i++)

                Transform(buffer);

            // Store hash in result pointer, and make sure we get in in the correct order on both endian models.
            for (int hashByte = 20; --hashByte >= 0;)
            {
                hash[hashByte] = (byte)((digest[hashByte >> 2] >> (((3 - hashByte) & 0x3) << 3)) & 0xff);
            }
        }*/
    }
}
