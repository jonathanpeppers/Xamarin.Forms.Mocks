using NUnit.Framework;
using System.Threading.Tasks;
using System.Linq;

namespace Xamarin.Forms.Mocks.Tests
{
    [TestFixture]
    public class NavigationTests
    {
        [SetUp]
        public void SetUp()
        {
            MockForms.Init();
        }

        [Test]
        public async Task Push()
        {
            var root = new ContentPage();
            var page = new ContentPage();
            await root.Navigation.PushAsync(page);
            Assert.AreEqual(root.Navigation.NavigationStack.Last(), page);
        }

        [Test]
        public async Task PushModal()
        {
            var root = new ContentPage();
            var page = new ContentPage();
            await root.Navigation.PushModalAsync(page);
            Assert.AreEqual(root.Navigation.ModalStack.Last(), page);
        }
    }
}

