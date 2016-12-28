using NUnit.Framework;

namespace Xamarin.Forms.Mocks.Tests
{
    [TestFixture]
    public class DeviceTests
    {
        [SetUp]
        public void SetUp()
        {
            MockForms.Init();
        }

        /// <summary>
        /// NOTE: not currently sure if this is the best idea yet or not
        /// </summary>
        [Test]
        public void BeginInvokeOnMainThreadIsSynchronous()
        {
            bool success = false;
            Device.BeginInvokeOnMainThread(() => success = true);
            Assert.IsTrue(success);
        }
    }
}
