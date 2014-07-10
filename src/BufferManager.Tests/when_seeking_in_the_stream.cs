using System;
using System.IO;
using NUnit.Framework;

namespace BufferManager.Tests
{
    [TestFixture]
    public class when_seeking_in_the_stream : has_buffer_pool_fixture
    {
        [Test]
        public void from_begin_sets_relative_to_beginning()
        {
            BufferPoolStream stream = new BufferPoolStream(BufferPool);
            stream.Write(new byte[500], 0, 500);
            stream.Seek(22, SeekOrigin.Begin);
            Assert.AreEqual(22, stream.Position);
        }

        [Test]
        public void from_end_sets_relative_to_end()
        {
            BufferPoolStream stream = new BufferPoolStream(BufferPool);
            stream.Write(new byte[500], 0, 500);
            stream.Seek(-100, SeekOrigin.End);
            Assert.AreEqual(400, stream.Position);
        }
        
        [Test]
        public void from_current_sets_relative_to_current()
        {
            BufferPoolStream stream = new BufferPoolStream(BufferPool);
            stream.Write(new byte[500], 0, 500);
            stream.Seek(-2, SeekOrigin.Current);
            stream.Seek(1, SeekOrigin.Current);
            Assert.AreEqual(499, stream.Position);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void a_negative_position_throws_an_argumentexception()
        {
            BufferPoolStream stream = new BufferPoolStream(BufferPool);
            stream.Seek(-1, SeekOrigin.Begin);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void seeking_past_end_of_stream_throws_an_argumentexception()
        {
            BufferPoolStream stream = new BufferPoolStream(BufferPool);
            stream.Write(new byte[500], 0, 500);
            stream.Seek(501, SeekOrigin.Begin);
        }
    }
}