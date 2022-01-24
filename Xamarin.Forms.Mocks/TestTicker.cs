using Microsoft.Maui.Animations;

namespace Xamarin.Forms.Mocks
{
    internal class TestTicker : Ticker
    {
        private bool _enabled;

        public override void Start()
        {
            _enabled = true;

            while (_enabled)
            {
                Fire();
            }
        }

        public override void Stop()
        {
            _enabled = false;
        }
    }
}
