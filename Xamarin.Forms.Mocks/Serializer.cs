using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Mocks
{
    internal class Serializer : IDeserializer
    {
        private IDictionary<string, object> _properties = new Dictionary<string, object>();

        public Task<IDictionary<string, object>> DeserializePropertiesAsync()
        {
            return Task.FromResult(_properties);
        }

        public Task SerializePropertiesAsync(IDictionary<string, object> properties)
        {
            _properties = properties;
            return Task.FromResult(true);
        }
    }
}