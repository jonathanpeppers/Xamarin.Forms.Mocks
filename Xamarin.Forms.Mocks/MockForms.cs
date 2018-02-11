using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms.Mocks.Xaml;

[assembly: InternalsVisibleTo("Xamarin.Forms.Mocks.Tests")]

namespace Xamarin.Forms.Mocks
{
    public static class MockForms
    {
        /// <summary>
        /// Callback for asserting against Device.OpenUri
        /// NOTE: MockForms.Init() clears this value
        /// </summary>
        public static Action<Uri> OpenUriAction { get; set; }

        public static void Init(string runtimePlatform = "Test", TargetIdiom idiom = default(TargetIdiom))
        {
            Device.PlatformServices = new PlatformServices(runtimePlatform);
            Device.Idiom = idiom;
            DependencyService.Register<SystemResourcesProvider>();
            DependencyService.Register<Serializer>();
            DependencyService.Register<ValueConverterProvider>();
            OpenUriAction = null;
        }
    }
}
