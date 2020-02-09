using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Mocks
{
    public class MockDeviceInfo : DeviceInfo
    {
        public override Size PixelScreenSize
        {
            get
            {
                if (CurrentOrientation == DeviceOrientation.Landscape)
                    return new Size(1080, 1920);
                else
                    return new Size(1920, 1080);
            }
        }

        public override Size ScaledScreenSize
        {
            get
            {
                var pixelSize = PixelScreenSize;
                return new Size(pixelSize.Width / ScalingFactor, pixelSize.Height / ScalingFactor);
            }
        }

        public override double ScalingFactor => 2;
    }
}
