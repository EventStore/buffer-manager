using System;
using NUnit.Framework;

namespace BufferManager.Tests
{
    [TestFixture]
    public class when_setting_the_length_of_the_pool : has_buffer_manager_fixture
    {
        [Test, ExpectedException(typeof(ArgumentException))]
        public void a_negative_length_throws_an_argumentexception()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            pool.SetLength(-1, false);
        }

        [Test]
        public void a_larger_length_makes_capacity_larger()
        {
            global::BufferManager.BufferManager manager = new global::BufferManager.BufferManager(10, 1000, 1);
            BufferPool pool = new BufferPool(1, manager);
            pool.SetLength(5000);
            Assert.AreNotEqual(5000, pool.Capacity);
        }

        [Test]
        public void length_is_set_when_setting_length()
        {
            BufferPool pool = new BufferPool(1, BufferManager);
            pool.SetLength(5000, false);
            Assert.AreEqual(5000, pool.Length);
        }

        [Test]
        public void a_smaller_length_lowers_capacity()
        {
            global::BufferManager.BufferManager manager = new global::BufferManager.BufferManager(10, 1000, 1);
            BufferPool pool = new BufferPool(5, manager);
            pool.SetLength(1);
            Assert.AreEqual(9, manager.AvailableBuffers);
        }

        [Test]
        public void a_smaller_length_checks_buffers_back_in_when_allowed()
        {
            global::BufferManager.BufferManager manager = new global::BufferManager.BufferManager(10, 1000, 1);
            BufferPool pool = new BufferPool(5, manager);
            pool.SetLength(1, true);
            Assert.AreEqual(9, manager.AvailableBuffers);
        }

        [Test]
        public void a_smaller_length_checks_buffers_back_in_when_not_allowed()
        {
            global::BufferManager.BufferManager manager = new global::BufferManager.BufferManager(10, 1000, 1);
            BufferPool pool = new BufferPool(5, manager);
            pool.SetLength(1, false);
            Assert.AreEqual(5, manager.AvailableBuffers);
        }
    }
}