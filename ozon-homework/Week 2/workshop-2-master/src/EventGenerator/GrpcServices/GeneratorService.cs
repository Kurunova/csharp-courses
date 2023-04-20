//  This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// 
//  PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
// 
//  Copyright ©  2022 Aleksey Kalduzov. All rights reserved
// 
//  Author: Aleksey Kalduzov
//  Email: alexei.kalduzov@gmail.com
// 
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Ozon.EventGenerator;

namespace EventGenerator.GrpcServices;

public class GeneratorService : Generator.GeneratorBase
{
    private readonly IEventStore _eventStore;
    private readonly ILogger<GeneratorService> _logger;

    public GeneratorService(IEventStore eventStore, ILogger<GeneratorService> logger)
    {
        _eventStore = eventStore;
        _logger = logger;
    }

    public override async Task EventStream(Empty request, IServerStreamWriter<EventResponse> responseStream, ServerCallContext context)
    {
        var random = new Random();

        try
        {
            while (!context.CancellationToken.IsCancellationRequested)
            {
                var delay = random.Next(100, 500);
                await Task.Delay(delay, context.CancellationToken);
                var result = GenerateRandomResult(random);
                await responseStream.WriteAsync(result, context.CancellationToken);
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("A operation was canceled");
        }
    }

    public override Task<GetStateResponse> GetState(GetStateRequest request, ServerCallContext context)
    {
        var result = new GetStateResponse();

        foreach (var id in request.Id)
        {
            if (context.CancellationToken.IsCancellationRequested)
            {
                break;
            }

            if (_eventStore.TryGetEvent(id, out var eventResponse))
            {
                result.Result.Add(
                    new StateResponse
                    {
                        Id = id,
                        State = ((EventResponse)eventResponse).State
                    });
            }
        }

        return Task.FromResult(result);
    }

    public override Task EventStreamDuplex(
        IAsyncStreamReader<EventStreamDuplexRequest> requestStream,
        IServerStreamWriter<EventResponse> responseStream,
        ServerCallContext context)
    {
        
        return base.EventStreamDuplex(requestStream, responseStream, context);
    }

    private EventResponse GenerateRandomResult(Random random)
    {
        var id = random.Next(0, 50);

        if (_eventStore.TryGetEvent(id, out var result))
        {
            ((EventResponse)result).State = ((EventResponse)result).State switch
            {
                State.Created => State.Updated,
                State.Updated => State.Deleted,
                _ => ((EventResponse)result).State
            };
        }
        else
        {
            var name = "Some string" + id;

            result = new EventResponse
            {
                Id = id,
                Name = name,
                State = State.Created,
                CreatedAt = Timestamp.FromDateTime(DateTime.UtcNow)
            };
            _eventStore.AddEvent(id, result);
        }

        return (EventResponse)result;
    }
}