using System;
using NUnit.Framework;

namespace BufferManager.Tests
{
    [TestFixture]
    public class when_reading_data_in_a_bufferpool_via_indexer : has_buffer_manager_fixture
    {
        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void if_the_index_is_past_the_length_an_argumentoutofrangeexception_is_thrown()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            var b = pool[3];
        }
    }
}