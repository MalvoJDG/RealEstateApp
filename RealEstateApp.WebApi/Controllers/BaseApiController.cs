﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RealEstateApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    }
}
