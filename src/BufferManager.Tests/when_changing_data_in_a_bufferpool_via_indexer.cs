using System;
using NUnit.Framework;

namespace BufferManager.Tests
{
    [TestFixture]
    public class when_changing_data_in_a_bufferpool_via_indexer : has_buffer_manager_fixture
    {
        [Test]
        public void an_index_under_zero_throws_an_argument_exception()
        {
            BufferPool pool = new BufferPool(12, BufferManager);
            Assert.Throws<ArgumentException>(() => pool[-1] = 4);
        }

        [Test]
        public void data_that_has_been_set_can_read()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            pool[3] = 5;
            Assert.AreEqual(5, pool[3]);
        }

        [Test]
        public void length_is_updated_when_index_higher_than_count_set()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            Assert.AreEqual(0, pool.Length);
            pool[3] = 5;
            Assert.AreEqual(4, pool.Length);
        }

        [Test]
        public void a_write_will_automatically_grow_the_buffer_pool()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            int initialCapacity = pool.Capacity;
            pool[initialCapacity + 14] = 5;
            Assert.AreEqual(initialCapacity * 2, pool.Capacity);
        }

        [Test]
        public void a_write_past_end_will_check_out_a_buffer_from_the_buffer_pool()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            int initial = BufferManager.AvailableBuffers;
            pool[pool.Capacity + 14] = 5;
            Assert.AreEqual(initial - 1, BufferManager.AvailableBuffers);
        }
    }
}