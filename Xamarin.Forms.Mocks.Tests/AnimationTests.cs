using NUnit.Framework;
using System.Threading.Tasks;

namespace Xamarin.Forms.Mocks.Tests
{
    [TestFixture]
    public class AnimationTests
    {
        [SetUp]
        public void SetUp()
        {
            MockForms.Init();
        }

        [Test]
        public async Task FadeTo()
        {
            var view = new BoxView();
            await view.FadeTo(0);
            Assert.AreEqual(0, view.Opacity);
        }
    }
}
