using NUnit.Framework;

namespace BufferManager.Tests
{
    [TestFixture]
    public class when_converting_to_a_byte_array : has_buffer_manager_fixture
    {
        [Test]
        public void the_byte_array_should_be_the_same_length_as_the_pool_with_data()
        {
            BufferPool pool = new BufferPool(5, BufferManager);
            for (int i = 0; i < 500; i++)
            {
                pool[i] = 12;
            }
            Assert.AreEqual(500, pool.ToByteArray().Length);
        }

        [Test]
        public void the_byte_array_should_have_the_same_data_as_the_pool_with_multiple_buffers()
        {
            BufferPool pool = new BufferPool(5, BufferManager);
            for (int i = 0; i < 5000; i++)
            {
                pool[i] = (byte)(i % 255);
            }
            byte[] data = pool.ToByteArray();
            for (int i = 0; i < 5000; i++)
            {
                Assert.AreEqual((byte)(i % 255), data[i]);
            }
        }

        [Test]
        public void the_byte_array_should_have_the_same_data_as_the_pool_with_a_single_buffer()
        {
            BufferPool pool = new BufferPool(5, BufferManager);
            for (int i = 0; i < 5; i++)
            {
                pool[i] = (byte)(i % 255);
            }
            byte[] data = pool.ToByteArray();
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual((byte)(i % 255), data[i]);
            }
        }


        [Test]
        public void an_empty_pool_should_return_an_empty_array()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            byte[] arr = pool.ToByteArray();
            Assert.AreEqual(0, arr.Length);
        }
    }
}