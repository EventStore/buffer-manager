using System;
using NUnit.Framework;

namespace BufferManager.Tests
{
    [TestFixture]
    public class when_reading_multiple_bytes : has_buffer_manager_fixture
    {
        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void a_null_read_buffer_throws_an_argumentnullexception()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            pool.ReadFrom(0, null, 0, 0);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void an_offset_larger_than_the_buffer_throws_an_argumentoutofrangeexception()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            pool.ReadFrom(0, new byte[5], 8, 3);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void a_count_larger_than_the_buffer_throws_an_argumentoutofrangeexception()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            pool.ReadFrom(0, new byte[5], 3, 5);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void a_negative_count_throws_an_argumentoutofrangeexception()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            pool.ReadFrom(0, new byte[5], 3, -1);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void a_negative_offset_throws_an_argumentoutofrangeexception()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            pool.ReadFrom(0, new byte[5], -1, 1);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void count_and_offset_together_lerger_than_buffer_throws_an_argumentoutofrangeexception()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            pool.ReadFrom(0, new byte[5], 4, 2);
        }

        [Test]
        public void reading_from_a_position_bigger_than_buffer_length_reads_nothing()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            pool[0] = 12;
            pool[1] = 13;
            int read = pool.ReadFrom(3, new byte[5], 0, 5);
            Assert.AreEqual(read, 0);
        }

        [Test]
        public void reading_from_a_position_plus_count_bigger_than_buffer_length_reads_the_right_amount()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            pool[0] = 12;
            pool[1] = 13;
            int read = pool.ReadFrom(0, new byte[5], 0, 5);
            Assert.AreEqual(read, 2);
        }

        [Test]
        public void can_read_within_a_single_buffer_with_no_offset()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            for (int i = 0; i < 255; i++)
            {
                pool[i] = (byte)i;
            }

            byte[] buffer = new byte[255];
            pool.ReadFrom(0, buffer, 0, 255);
            for (int i = 0; i < 255; i++)
            {
                Assert.AreEqual((byte)i, buffer[i]);
            }
        }

        [Test]
        public void can_read_from_multiple_buffers()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            for (int i = 0; i < 5000; i++)
            {
                pool[i] = (byte)(i % 255);
            }

            byte[] buffer = new byte[5000];
            pool.ReadFrom(0, buffer, 0, 5000);
            for (int i = 0; i < 5000; i++)
            {
                Assert.AreEqual((byte)(i % 255), buffer[i]);
            }
        }

        [Test]
        public void can_read_using_an_offset()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            for (int i = 5; i < 260; i++)
            {
                pool[i] = (byte)(i - 5);
            }

            byte[] buffer = new byte[255];
            pool.ReadFrom(5, buffer, 0, 255);
            for (int i = 0; i < 255; i++)
            {
                Assert.AreEqual((byte)i, buffer[i]);
            }
        }
    }
}