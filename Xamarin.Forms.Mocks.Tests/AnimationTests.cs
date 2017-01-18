using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Xamarin.Forms.Mocks.Tests
{
    [TestFixture]
    public class AnimationTests
    {
        private const int Timeout = 1000;

        [SetUp]
        public void SetUp()
        {
            MockForms.Init();
        }

        [Test, Timeout(Timeout)]
        public async Task FadeTo()
        {
            var view = new BoxView();
            await view.FadeTo(0);
            Assert.AreEqual(0, view.Opacity);
        }

        [Test, Timeout(Timeout)]
        public async Task ParallelFadeTo()
        {
            var a = new BoxView();
            var b = new BoxView();
            var list = new List<Task>
            {
                a.FadeTo(0),
                b.FadeTo(0)
            };
            await Task.WhenAll(list);

            Assert.AreEqual(0, a.Opacity);
            Assert.AreEqual(0, b.Opacity);
        }

        [Test, Timeout(Timeout)]
        public async Task AnimationWithFinished()
        {
            var source = new TaskCompletionSource<bool>();

            var view = new BoxView();
            var animation = new Animation(v =>
            {
                view.Opacity = v;

            }, 1, 0);
            view.Animate("Fade", animation, finished: (a, b) => source.TrySetResult(true));

            await source.Task;
            Assert.AreEqual(0, view.Opacity);
        }

        [Test, Timeout(Timeout)]
        public async Task AnimationWithRepeatFinished()
        {
            var source = new TaskCompletionSource<bool>();

            int count = 0;
            var view = new BoxView();
            var animation = new Animation(v =>
            {
                view.Opacity = v;

            }, 1, 0);
            view.Animate("Fade", animation, finished: (a, b) => source.TrySetResult(true), repeat: () => ++count == 3);

            await source.Task;
            Assert.AreEqual(0, view.Opacity);
        }
    }
}
