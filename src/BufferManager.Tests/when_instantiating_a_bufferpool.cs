using System;
using NUnit.Framework;

namespace BufferManager.Tests
{
    [TestFixture]
    public class when_instantiating_a_bufferpool : has_buffer_manager_fixture
    {
        [Test]
        public void a_negative_initial_buffers_throws_an_argumentexception()
        {
            Assert.Throws<ArgumentException>(() => new BufferPool(-1, BufferManager));
        }

        [Test]
        public void a_null_buffer_manager_throws_an_argumentnullexception()
        {
            Assert.Throws<ArgumentNullException>(() => new BufferPool(12, null));
        }

        [Test]
        public void an_empty_buffer_has_a_length_of_zero()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            Assert.AreEqual(0, pool.Length);
        }

        [Test]
        public void the_requested_buffers_should_be_removed_from_the_buffer_manager()
        {
            int initialBuffers = BufferManager.AvailableBuffers;
            new BufferPool(10, BufferManager);
            Assert.AreEqual(initialBuffers - 10, BufferManager.AvailableBuffers);
        }
    }
}