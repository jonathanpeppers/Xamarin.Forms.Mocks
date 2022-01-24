using Microsoft.Maui.Controls;
using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms.Mocks.Xaml;

[assembly: InternalsVisibleTo("Xamarin.Forms.Mocks.Tests")]

namespace Xamarin.Forms.Mocks
{
    public static class MockForms
    {
        /// <summary>
        /// Callback for asserting against Microsoft.Maui.Essentials.Launcher.OpenAsync
        /// NOTE: MockForms.Init() clears this value
        /// </summary>
        public static Action<Uri> OpenUriAction { get; set; }

        public static void Init(string runtimePlatform = "Test", TargetIdiom idiom = default(TargetIdiom), OSAppTheme requestedTheme = OSAppTheme.Unspecified)
        {
            Device.PlatformServices = new PlatformServices(runtimePlatform, requestedTheme);
            Device.Info = new MockDeviceInfo();
            DependencyService.Register<SystemResourcesProvider>();
            DependencyService.Register<Serializer>();
            OpenUriAction = null;
        }
    }
}
