using System;
using NUnit.Framework;

namespace BufferManager.Tests
{
    [TestFixture]
    public class when_checking_out_a_buffer
    {
        [Test]
        public void should_return_a_valid_buffer_when_available()
        {
            global::BufferManager.BufferManager manager = new global::BufferManager.BufferManager(1, 1000, 1);
            ArraySegment<byte> buffer = manager.CheckOut();
            Assert.AreEqual(1000, buffer.Count);
        }

        [Test]
        public void should_decrement_available_buffers()
        {
            global::BufferManager.BufferManager manager = new global::BufferManager.BufferManager(1, 1000, 1);
            manager.CheckOut();
            Assert.AreEqual(0, manager.AvailableBuffers);
        }

        [Test]
        public void should_create_a_segment_if_none_are_availabke()
        {
            global::BufferManager.BufferManager manager = new global::BufferManager.BufferManager(10, 1000, 0);
            manager.CheckOut();
            Assert.AreEqual(9, manager.AvailableBuffers);
        }

        [Test]
        public void should_throw_an_unabletocreatememoryexception_if_acquiring_memory_is_disabled_and_out_of_memory()
        {
            global::BufferManager.BufferManager manager = new global::BufferManager.BufferManager(1, 1000, 1, false);
            manager.CheckOut();
            //should be none left, boom
            Assert.Throws<UnableToCreateMemoryException>(() => manager.CheckOut());
        }
    }
}