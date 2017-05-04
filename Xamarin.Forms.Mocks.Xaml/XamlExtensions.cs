using System;

namespace Xamarin.Forms.Xaml
{
    public static class XamlExtensions
    {
        public static void LoadFromXaml(this object view, string xaml)
        {
            XamlLoader.Load(view, xaml);
        }
    }
}
