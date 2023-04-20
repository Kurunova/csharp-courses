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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Ozon.EventGenerator;
using OzonEdu.EventClient.BL;
using OzonEdu.EventClient.Models;

namespace OzonEdu.EventClient.Controllers;

[Route("events")]
public class EventsController : Controller
{
    private readonly IEventStorage _storage;
    private readonly Generator.GeneratorClient _generatorClient;

    public EventsController(IEventStorage storage, Generator.GeneratorClient generatorClient, IServiceProvider provider, IServer server)
    {
        _storage = storage;
        _generatorClient = generatorClient;
    }

    [HttpGet("{id:int}")] // url/events/id
    public async Task<ActionResult<EventState>> GetStateAsync(int id)
    {
        var request = new GetStateRequest
        {
            Id =
            {
                id
            }
        };

        var events = await _generatorClient.GetStateAsync(request);

        if (events.Result.Count == 0)
        {
            return NotFound(id);
        }

        var @event = events.Result.First();

        var eventState = new EventState
        {
            Id = @event.Id,

            State = @event.State switch
            {
                State.Created => StateType.Created,
                State.Updated => StateType.Updated,
                State.Deleted => StateType.Deleted
            },
            LastUpdateState = DateTime.UtcNow
        };

        return Ok(eventState);
    }

    [HttpGet] // url/events/id
    public async Task<ActionResult<List<EventState>>> GetStatesAsync([FromQuery(Name = "id")] List<int> ids)
    {
        var list = new List<EventState>(ids.Count);

        foreach (var id in ids)
        {
            list.Add(
                new EventState
                {
                    Id = id
                });
        }

        return Ok(list);
    }

    [HttpGet("{state}")]
    public async Task<ActionResult<int>> GetCountEventsByStateAsync(StateType state)
    {
        return Ok(_storage.GetCountEventsByState(state));
    }
}