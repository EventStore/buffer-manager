using System;
using NUnit.Framework;

namespace BufferManager.Tests
{
    [TestFixture]
    public class when_writing_multiple_bytes : has_buffer_manager_fixture
    {
        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void a_null_byte_array_throws_an_argumentnullexception()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            pool.Append(null);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void an_offset_larger_than_the_buffer_throws_an_argumentoutofrangeexception()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            pool.Write(0, new byte[5], 8, 3);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void a_count_larger_than_the_buffer_throws_an_argumentoutofrangeexception()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            pool.Write(0, new byte[5], 3, 5);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void a_negative_count_throws_an_argumentoutofrangeexception()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            pool.Write(0, new byte[5], 3, -1);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void a_negative_offset_throws_an_argumentoutofrangeexception()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            pool.Write(0, new byte[5], -1, 1);
        }

        [Test]
        public void length_is_updated_to_include_bytes_written()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            byte[] data = { 1, 2, 3, 4, 5 };
            pool.Append(data);
            Assert.IsTrue(pool.Length == 5);
        }

        [Test]
        public void data_is_written_to_the_internal_buffer()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            byte[] data = { 1, 2, 3, 4, 5 };
            pool.Append(data);
            for (byte i = 0; i < 5; i++)
            {
                Assert.AreEqual(i + 1, pool[i]);
            }
        }

        [Test]
        public void pool_can_expand_capacity()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            int initialCapacity = pool.Capacity;
            byte[] data = new byte[initialCapacity + 25];
            pool.Append(data);
            Assert.AreEqual(initialCapacity * 2, pool.Capacity);
        }

        [Test]
        public void can_write_given_a_self_offset()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            byte[] data = { 1, 2, 3, 4, 5 };
            pool.Write(4, data, 0, 5); //start at position 4
            for (byte i = 4; i < 9; i++)
            {
                Assert.AreEqual(i - 3, pool[i]);
            }
        }

        [Test]
        public void can_write_given_a_source_offset()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            byte[] data = { 1, 2, 3, 4, 5 };
            pool.Write(0, data, 3, 2);
            Assert.AreEqual(pool[0], 4);
            Assert.AreEqual(pool[1], 5);
        }
    }
}