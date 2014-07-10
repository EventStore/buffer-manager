using System;
using NUnit.Framework;

namespace BufferManager.Tests
{
    [TestFixture]
    public class when_a_buffer_pool_has_been_disposed : has_buffer_manager_fixture
    {
        private BufferPool m_DisposedPool;

        public override void Setup()
        {
            base.Setup();
            m_DisposedPool = new BufferPool(10, BufferManager);
            m_DisposedPool.Dispose();
        }

        [Test, ExpectedException(typeof(ObjectDisposedException))]
        public void reading_indexer_throws_objectdisposedexception()
        {
            byte b = m_DisposedPool[0];
        }

        [Test, ExpectedException(typeof(ObjectDisposedException))]
        public void writing_indexer_throws_objectdisposedexception()
        {
            m_DisposedPool[0] = 5;
        }

        [Test, ExpectedException(typeof(ObjectDisposedException))]
        public void writing_multiple_bytes_throws_objectdisposedexception()
        {
            m_DisposedPool.Append(new byte[] { 1, 2, 3, 4 });
        }


        [Test, ExpectedException(typeof(ObjectDisposedException))]
        public void effective_enumerator_throws_objectdisposedexception()
        {
            foreach (ArraySegment<byte> segment in m_DisposedPool.EffectiveBuffers)
            {
            }
        }

        [Test, ExpectedException(typeof(ObjectDisposedException))]
        public void setting_length_throws_objectdisposedexception()
        {
            m_DisposedPool.SetLength(200);
        }

        [Test, ExpectedException(typeof(ObjectDisposedException))]
        public void converting_to_a_byte_array_throws_objectdisposedexception()
        {
            m_DisposedPool.ToByteArray();
        }

    }
}