using NUnit.Framework;
using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.Mocks.Tests
{
    [TestFixture]
    public class LoadFromXamlTests
    {
        [SetUp]
        public void SetUp()
        {
            MockForms.Init();
        }

        [Test]
        public void LoadFromXaml()
        {
            var label = new Label();
            label.LoadFromXaml("<Label Text=\"Woot\" />");
            Assert.AreEqual("Woot", label.Text);
        }

        [Test]
        public void MarkupExtension()
        {
            var label = new Label();
            label.LoadFromXaml("<Label xmlns:f=\"clr-namespace:Xamarin.Forms.Mocks.Tests;assembly=Xamarin.Forms.Mocks.Tests\" Text=\"{f:Terrible}\" />");
            Assert.AreEqual("2016", label.Text);
        }

        [Test]
        public void LoadViaNew()
        {
            var view = new TestView();
            var label = (Label)view.Content;
            Assert.AreEqual("Woot", label.Text);
        }

        [Test]
        public void LoadViaNewCompiled()
        {
            var view = new TestViewCompiled();
            var label = (Label)view.Content;
            Assert.AreEqual("Compiled", label.Text);
        }
    }
}
