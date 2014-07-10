using System;
using NUnit.Framework;

namespace BufferManager.Tests
{
    [TestFixture]
    public class when_checking_in_a_buffer
    {
        [Test]
        public void should_accept_a_checked_out_buffer()
        {
            global::BufferManager.BufferManager manager = new global::BufferManager.BufferManager(10, 1000, 0);
            manager.CheckIn(manager.CheckOut());
        }

        [Test]
        public void should_increment_available_buffers()
        {
            global::BufferManager.BufferManager manager = new global::BufferManager.BufferManager(10, 1000, 0);
            manager.CheckIn(manager.CheckOut());
            Assert.AreEqual(10, manager.AvailableBuffers);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_argumentnullexception_if_null_buffer()
        {
            global::BufferManager.BufferManager manager = new global::BufferManager.BufferManager(10, 1000, 0);
            manager.CheckIn(null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void should_throw_argumentexception_if_buffer_wrong_size()
        {
            global::BufferManager.BufferManager manager = new global::BufferManager.BufferManager(10, 1000, 0);
            byte[] data = new byte[10000];
            manager.CheckIn(new ArraySegment<byte>(data));
        }
    }
}