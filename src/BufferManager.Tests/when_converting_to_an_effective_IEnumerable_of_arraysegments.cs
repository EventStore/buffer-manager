using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace BufferManager.Tests
{
    [TestFixture]
    public class when_converting_to_an_effective_IEnumerable_of_arraysegments : has_buffer_manager_fixture
    {
        [Test]
        public void empty_returns_no_results()
        {
            BufferPool pool = new BufferPool(10, BufferManager);
            foreach (ArraySegment<byte> effectiveBuffer in pool.EffectiveBuffers)
            {
                Assert.Fail("should not have been buffers");
            }
        }

        [Test]
        public void a_single_partial_segment_can_be_returned()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            for (byte i = 0; i < 10; i++)
            {
                pool[i] = i;
            }
            List<ArraySegment<byte>> buffers = new List<ArraySegment<byte>>(pool.EffectiveBuffers);
            Assert.IsTrue(buffers.Count == 1);
            for (byte i = 0; i < 10; i++)
            {
                Assert.IsTrue(buffers[0].Array[buffers[0].Offset + i] == i);
            }
        }

        [Test]
        public void multiple_segments_can_be_returned()
        {
            global::BufferManager.BufferManager manager = new global::BufferManager.BufferManager(3, 1000, 1);
            BufferPool pool = new BufferPool(10, manager);
            for (int i = 0; i < 2500; i++)
            {
                pool[i] = (byte)(i % 255);
            }
            List<ArraySegment<byte>> buffers = new List<ArraySegment<byte>>(pool.EffectiveBuffers);
            Assert.IsTrue(buffers.Count == 3);
            Assert.IsTrue(buffers[0].Count == 1000);
            Assert.IsTrue(buffers[1].Count == 1000);
            Assert.IsTrue(buffers[2].Count == 500);
        }
    }
}