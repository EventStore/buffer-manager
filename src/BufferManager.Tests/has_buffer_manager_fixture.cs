using NUnit.Framework;

namespace BufferManager.Tests
{
    public class has_buffer_manager_fixture
    {
        protected BufferManager BufferManager;

        [SetUp]
        public virtual void Setup()
        {
            BufferManager = new BufferManager(128, 1024, 1);
        }
    }
}
