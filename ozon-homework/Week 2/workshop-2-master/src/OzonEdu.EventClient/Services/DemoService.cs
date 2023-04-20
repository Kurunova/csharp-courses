using Microsoft.Extensions.Options;
using OzonEdu.EventClient.Options;

namespace OzonEdu.EventClient.Services
{
    class DemoService : IDemoService
    {
        private readonly StoreConfig _options;

        public DemoService(IOptions<StoreConfig> options)
        {
            _options = options.Value;
        }

        public string Env => _options.Name;
    }
}