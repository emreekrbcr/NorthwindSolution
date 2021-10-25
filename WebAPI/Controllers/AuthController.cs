using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete.Dtos;
using Core.Utilities.WebAPI;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userForLoginResult = _authService.Login(userForLoginDto);
            IActionResult loginResult = ApiHelper.CheckRequestResult(userForLoginResult);

            if (loginResult.GetType() == typeof(BadRequestObjectResult)) //Giriş hatalı olduysa
            {
                return loginResult;
            }

            //Giriş başarılıysa devam ediyoruz ve token üretiyoruz

            IActionResult createTokenResult =
                ApiHelper.CheckRequestResult(_authService.CreateAccessToken(userForLoginResult.Data));

            return createTokenResult;
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userForRegisterResult = _authService.Register(userForRegisterDto);
            IActionResult registerResult = ApiHelper.CheckRequestResult(userForRegisterResult);

            if (registerResult.GetType() == typeof(BadRequestObjectResult))
            {
                return registerResult;
            }

            IActionResult createTokenResult =
                ApiHelper.CheckRequestResult(_authService.CreateAccessToken(userForRegisterResult.Data));
            return createTokenResult;

        }
    }
}
