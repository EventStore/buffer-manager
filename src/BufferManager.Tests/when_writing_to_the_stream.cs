using NUnit.Framework;

namespace BufferManager.Tests
{
    [TestFixture]
    public class when_writing_to_the_stream : has_buffer_pool_fixture
    {
        [Test]
        public void position_is_incremented()
        {
            BufferPoolStream stream = new BufferPoolStream(BufferPool);
            stream.Write(new byte[500], 0, 500);
            Assert.AreEqual(500, stream.Position);
        }

    }
}