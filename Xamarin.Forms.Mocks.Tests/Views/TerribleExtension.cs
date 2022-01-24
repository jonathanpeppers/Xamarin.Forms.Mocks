using Microsoft.Maui.Controls.Xaml;
using System;

namespace Xamarin.Forms.Mocks.Tests
{
    public class TerribleExtension : IMarkupExtension<string>
    {
        public string ProvideValue(IServiceProvider serviceProvider)
        {
            return "2016";
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }
    }
}
