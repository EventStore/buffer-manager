using NUnit.Framework;

namespace BufferManager.Tests
{
    [TestFixture]
    public class when_disposing_a_buffer_pool : has_buffer_manager_fixture
    {
        [Test]
        public void buffers_are_released_back_to_the_buffer_pool()
        {
            int initial = BufferManager.AvailableBuffers;
            using (new BufferPool(20, BufferManager))
            {
                //sanity check (make sure they are actually gone)
                Assert.AreEqual(initial - 20, BufferManager.AvailableBuffers);
            }
            Assert.AreEqual(initial, BufferManager.AvailableBuffers);
        }
    }
}