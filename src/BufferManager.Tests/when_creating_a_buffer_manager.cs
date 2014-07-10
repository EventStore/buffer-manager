using System;
using NUnit.Framework;

namespace BufferManager.Tests
{
    [TestFixture]
    public class when_creating_a_buffer_manager
    {
        [Test]
        public void a_zero_chunk_size_causes_an_argumentexception()
        {
            Assert.Throws<ArgumentException>(() => new global::BufferManager.BufferManager(1024, 0, 1024));
        }

        [Test]
        public void a_negative_chunk_size_causes_an_argumentexception()
        {
            Assert.Throws<ArgumentException>(() => new global::BufferManager.BufferManager(200, -1, 200));
        }

        [Test]
        public void a_negative_chunks_per_segment_causes_an_argumentexception()
        {
            Assert.Throws<ArgumentException>(() => new global::BufferManager.BufferManager(-1, 1024, 8));
        }

        [Test]
        public void a_zero_chunks_per_segment_causes_an_argumentexception()
        {
            Assert.Throws<ArgumentException>(() => new global::BufferManager.BufferManager(0, 1024, 8));
        }

        [Test]
        public void a_negative_number_of_segments_causes_an_argumentexception()
        {
            Assert.Throws<ArgumentException>(() => new global::BufferManager.BufferManager(1024, 1024, -1));
        }

        [Test]
        public void can_create_a_manager_with_zero_inital_segments()
        {
            Assert.DoesNotThrow(() => new global::BufferManager.BufferManager(1024, 1024, 0));
        }
    }
}
