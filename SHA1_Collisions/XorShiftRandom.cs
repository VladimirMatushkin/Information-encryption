using System.Collections.Generic;

namespace SHA1_Collisions
{
    class XorShiftRandom
    {
        private static int FillBufferMultiple = 4;

        private uint _x = 123456789;
        private uint _y = 362436069;
        private uint _z = 521288629;
        private uint _w = 88675123;

        private Queue<byte> _bytes = new Queue<byte>(128);

        public void NextBytes(byte[] buffer)
        {
            int offset = 0;
            while (_bytes.Count != 0 && offset < buffer.Length)
                buffer[offset++] = _bytes.Dequeue();

            int length = ((buffer.Length - offset) / FillBufferMultiple) * FillBufferMultiple;
            if (length > 0)
                FillBuffer(buffer, offset, offset + length);

            offset += length;
            while (offset < buffer.Length)
            {
                if (_bytes.Count == 0)
                {
                    uint t = _x ^ (_x << 11);
                    _x = _y; _y = _z; _z = _w;
                    _w = _w ^ (_w >> 19) ^ (t ^ (t >> 8));
                    _bytes.Enqueue((byte)(_w & 0xFF));
                    _bytes.Enqueue((byte)((_w >> 8) & 0xFF));
                    _bytes.Enqueue((byte)((_w >> 16) & 0xFF));
                    _bytes.Enqueue((byte)((_w >> 24) & 0xFF));
                }
                buffer[offset++] = _bytes.Dequeue();
            }
        }

        unsafe void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            uint x = _x, y = _y, z = _z, w = _w; // copy the state into locals temporarily
            fixed (byte* pbytes = buf)
            {
                uint* pbuf = (uint*)(pbytes + offset);
                uint* pend = (uint*)(pbytes + offsetEnd);
                while (pbuf < pend)
                {
                    uint t = x ^ (x << 11);
                    x = y; y = z; z = w;
                    *(pbuf++) = w = w ^ (w >> 19) ^ (t ^ (t >> 8));
                }
            }
            _x = x; _y = y; _z = z; _w = w;
        }
    }
}
