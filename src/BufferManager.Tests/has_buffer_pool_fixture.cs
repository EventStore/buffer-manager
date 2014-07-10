using NUnit.Framework;

namespace BufferManager.Tests
{
    public abstract class has_buffer_pool_fixture : has_buffer_manager_fixture
    {
        protected BufferPool BufferPool;

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            BufferPool = new BufferPool(10, BufferManager);
        }
    }
}
