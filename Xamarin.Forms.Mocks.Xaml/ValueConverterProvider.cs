using System;
using System.Reflection;
using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.Mocks.Xaml
{
    public class ValueConverterProvider : IValueConverterProvider
    {
        public object Convert(object value, Type toType, Func<MemberInfo> minfoRetriever, IServiceProvider serviceProvider)
        {
            return value.ConvertTo(toType, minfoRetriever, serviceProvider);
        }
    }
}
