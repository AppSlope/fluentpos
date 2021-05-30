using FluentPOS.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;

namespace FluentPOS.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController<T> : ControllerBase
    {
        private IMediator _mediatorInstance;
        private ILogger<T> _loggerInstance;
        protected IMediator _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
        protected ILogger<T> _logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();
        //Easiest way to Get Current UserId in Controller Layer                           
        protected int GetUserId()
        {
            return GetClaimsValue(ClaimTypes.NameIdentifier, value => int.TryParse(value, out var id) ? id : throw new AuthorizationException());
        }

        private V GetClaimsValue<V>(string claimName, Func<string, V> parseClaimValue)
        {
            var claim = User.FindFirst(claimName);
            if (claim != null)
            {
                return parseClaimValue(claim.Value);
            }
            return default;
        }
    }
}