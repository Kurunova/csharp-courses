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

using EventGenerator.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventGenerator.Controllers;

[Route("events")]
public class EventsController : ControllerBase
{
    private readonly IEventStore _eventStore;

    public EventsController(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    [HttpGet]
    public Task<EventResponse> GetEvent()
    {
        var random = new Random();
        var result = GenerateRandomResult(random);

        return Task.FromResult(result);
    }

    private EventResponse GenerateRandomResult(Random random)
    {
        var id = random.Next(0, 1000);

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
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _eventStore.AddEvent(id, result);
        }

        return (EventResponse)result;
    }
}