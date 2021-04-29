using System;
using System.Data.Common;
using System.Threading.Tasks;
using BukashkaCo.Finance.Api.ResourceModels;
using BukashkaCo.Finance.Domain.Abstraction;
using BukashkaCo.Finance.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BukashkaCo.Finance.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        public UserController(IAuthService authService, IRegistrationService registrationService)
        {
            _authService = authService;
            _registrationService = registrationService;
        }

        private readonly IRegistrationService _registrationService;

        private readonly IAuthService _authService;

        [HttpPost]
        [AllowAnonymous]
        [Route("/api/[controller]/register")]
        public async Task<ActionResult<UserModel>> Register([FromBody]RegisterRequest request)
        {
            try
            {
                return await _registrationService.Register(request);
            }
            catch (Exception exception)
            {
                throw await ConvertResponseException(exception);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserModel>> Login([FromBody] LoginRequest request)
        {
            try
            {
                return await _authService.Login(request);
            }
            catch (Exception exception)
            {
                throw await ConvertResponseException(exception);
            }
        }

        private async Task<HttpResponseException> ConvertResponseException(Exception exception)
        {
            if (exception is DbException)
            {
                Error serverError = new Error()
                {
                    Code = 503,
                    Message = " "
                };
                return new HttpResponseException()
                {
                    StatusCode = 200,
                    Error = serverError
                };
            }
                
            Error error = new Error()
            {
                Code = 400,
                Message = exception.Message
            };

            return new HttpResponseException()
            {
                StatusCode = 200,
                Error = error 
            }; 
        }
    }
}