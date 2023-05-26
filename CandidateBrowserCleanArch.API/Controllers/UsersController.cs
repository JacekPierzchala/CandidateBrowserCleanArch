﻿using CandidateBrowserCleanArch.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CandidateBrowserCleanArch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy =CustomRoleClaims.UserUpdate)]
        [HttpGet]
        public async Task<ActionResult<ServiceReponse<IEnumerable<ReadUserListDto>>>>GetUsers()
        {
            var response = await _mediator.Send(new GetUsersRequest());
            return Ok(response);
        }

        [Authorize(Policy = CustomRoleClaims.UserUpdate)]
        [HttpGet("byId/{userId}")]
        public async Task<ActionResult<ServiceReponse<ReadUserDetailsDto>>> GetUserDetails(string userId)
        {
            var response = await _mediator.Send(new GetUserDetailsRequest {  UserId=userId});
            return Ok(response);
        }
    }
}