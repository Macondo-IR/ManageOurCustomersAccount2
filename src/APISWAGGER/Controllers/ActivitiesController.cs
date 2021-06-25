using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APISWAGGER.Controllers
{
    //[ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/activities")]

    public class ActivitiesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List.ActivitiesEnvelope>> List(int? limit, 
            int? offset, bool isGoing, bool isHost, DateTime? startDate)
        {
            return await Mediator.Send(new List.Query(limit, 
                offset, isGoing, isHost, startDate));
        }
        [HttpGet("activities/{id}")]

        //[HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ActivityDto>> Details(Guid id)
        {
            return await Mediator.Send(new Details.Query{Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        //[HttpPut("{id}")]
        [HttpPut("activities/{id}")]
        [Authorize(Policy = "IsActivityHost")]
        public async Task<ActionResult<Unit>> Edit(Guid id, Edit.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        //[HttpDelete("{id}")]
        [HttpDelete("activities/{id}")]
        [Authorize(Policy = "IsActivityHost")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new Delete.Command{Id = id});
        }

        //[HttpPost("{id}/attend")]
        [HttpPost("activities/{id}/attend")]
        public async Task<ActionResult<Unit>> Attend(Guid id)
        {
            return await Mediator.Send(new Attend.Command{Id = id});
        }

        //[HttpDelete("{id}/attend")]
        [HttpDelete("activities/{id}/attend")]
        public async Task<ActionResult<Unit>> Unattend(Guid id)
        {
            return await Mediator.Send(new Unattend.Command{Id = id});
        }
    }
}