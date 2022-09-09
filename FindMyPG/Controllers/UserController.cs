using AutoMapper;
using FindMyPG.Controllers.Base;
using FindMyPG.Core.Entities;
using FindMyPG.Models;
using FindMyPG.Service.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using FindMyPG.Core.Enums;
using FindMyPG.Service.Authentications;
using FindMyPG.Models.Responses;

namespace FindMyPG.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        public UserController(IUserService userService, IMapper mapper, IAuthenticationService authenticationService)
        {
            _authenticationService=authenticationService;
            _userService = userService;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> PostRegister(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var userRegistrationRequest = new UserRegistrationRequest(userModel.FirstName, userModel.LastName, userModel.PhoneNumber, userModel.Email, userModel.Password, userModel.Role);
                var result = await _userService.RegisterUser(userRegistrationRequest);
                if (result.Success)
                    return OkResult(result.User);
                return BadRequestResult(result.Errors, "");
            }
            return BadRequestResult(ModelState);
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> PostLogin(Login login)
        {
            if (ModelState.IsValid)
            {
                var response = new LoginSuccessResponse();
                var loginresult = await _userService.ValidateUser(login.UserName, login.Password);
                switch (loginresult)
                {
                    case LoginResultEnum.Successful:
                        var user = await _userService.GetUserByUserName(login.UserName);
                        var result = await _authenticationService.AuthenticateUser(user);
                        if(result.Success)
                        {
                            response.Token = result.AccessToken.Token;
                            response.Expirein = result.AccessToken.ExpireIn;
                        }
                        else
                        {
                            foreach(var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error);
                            }
                        }
                        break;
                    case LoginResultEnum.UserNotExist:
                        ModelState.AddModelError(string.Empty, "Wrong Credential");
                        break;
                    case LoginResultEnum.WrongPassword:
                        ModelState.AddModelError(string.Empty, "Wrong Credential");
                        break;
                    case LoginResultEnum.LockedOut:
                        ModelState.AddModelError(string.Empty, "Wrong Credential");
                        break;
                }
                if(ModelState.IsValid)
                    return OkResult(response);
            }
            return BadRequestResult(ModelState);
        }
    }
}
