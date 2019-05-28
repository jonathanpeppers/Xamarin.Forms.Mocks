using System;
using Xamarin.Forms.Xaml;

namespace TestAssembly
{
    public class FooExtension : IMarkupExtension<string>
    {
        public string ProvideValue (IServiceProvider serviceProvider)
        {
            return "Bar";
        }

        object IMarkupExtension.ProvideValue (IServiceProvider serviceProvider)
        {
            return ProvideValue (serviceProvider);
        }
    }
}
