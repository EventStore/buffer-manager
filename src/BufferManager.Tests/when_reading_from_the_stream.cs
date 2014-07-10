using System.IO;
using NUnit.Framework;

namespace BufferManager.Tests
{
    [TestFixture]
    public class when_reading_from_the_stream : has_buffer_pool_fixture
    {
        [Test]
        public void position_is_incremented()
        {
            BufferPoolStream stream = new BufferPoolStream(BufferPool);
            stream.Write(new byte[500], 0, 500);
            stream.Seek(0, SeekOrigin.Begin);
            Assert.AreEqual(0, stream.Position);
            stream.Read(new byte[50], 0, 50);
            Assert.AreEqual(50, stream.Position);
        }

        [Test]
        public void a_read_past_the_end_of_the_stream_returns_zero()
        {
            BufferPoolStream stream = new BufferPoolStream(BufferPool);
            stream.Write(new byte[500], 0, 500);
            stream.Position = 0;
            int read = stream.Read(new byte[500], 0, 500);
            Assert.AreEqual(500, read);
            read = stream.Read(new byte[500], 0, 500);
            Assert.AreEqual(0, read);
        }

        [Test]
        public void reading_from_the_stream_with_StreamCopyTo_returns_all_data()
        {
            BufferPoolStream stream = new BufferPoolStream(BufferPool);
            int size = 20123;
            stream.Write(new byte[size], 0, size);
            stream.Position = 0;

            var destination = new MemoryStream();
            stream.CopyTo(destination);
            Assert.AreEqual(destination.Length, size);
        }
    }
}