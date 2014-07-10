using System;

namespace BufferManager
{
    public class UnableToAllocateBufferException : Exception
    {
        public UnableToAllocateBufferException()
            : base("Couldn't allocate buffer after few trials.")
        {
        }
    }
}
