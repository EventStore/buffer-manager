using System;
using NUnit.Framework;

namespace BufferManager.Tests
{
    [TestFixture]
    public class when_insantiating_a_buffer_pool_stream : has_buffer_pool_fixture
    {
        [Test]
        public void a_null_buffer_pool_throws_an_argumentnullexception()
        {
            Assert.Throws<ArgumentNullException>(() => new BufferPoolStream(null));
        }

        [Test]
        public void the_internal_buffer_pool_is_set()
        {
            BufferPoolStream stream = new BufferPoolStream(BufferPool);
            Assert.AreEqual(BufferPool, stream.BufferPool);
        }

    }
}