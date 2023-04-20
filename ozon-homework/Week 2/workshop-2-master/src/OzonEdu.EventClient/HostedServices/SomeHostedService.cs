using System;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ozon.EventGenerator;
using OzonEdu.EventClient.BL;

namespace OzonEdu.EventClient.HostedServices
{
    public class SomeHostedService : BackgroundService
    {
        private readonly IEventStorage _storage;
        private readonly IServiceProvider _provider;

        public SomeHostedService(IEventStorage storage, IServiceProvider provider)
        {
            _storage = storage;
            _provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await using var scope = _provider.CreateAsyncScope();
            var client = scope.ServiceProvider.GetRequiredService<Generator.GeneratorClient>();
            using var eventResponseStream = client.EventStream(new Empty(), cancellationToken: stoppingToken);

            while (await eventResponseStream.ResponseStream.MoveNext(stoppingToken))
            {
                var e = eventResponseStream.ResponseStream.Current;
                
                _storage.AddEvent(new Event
                {
                    Id = e.Id,
                    Data = e.Name,
                    State = e.State switch
                    {
                        State.Created => StateType.Created,
                        State.Updated => StateType.Updated,
                        State.Deleted => StateType.Deleted
                    },
                    CreatedAt = e.CreatedAt.ToDateTime(),
                    UpdateAt = e.UpdatedAt?.ToDateTime()
                });
            }

        }
    }
}