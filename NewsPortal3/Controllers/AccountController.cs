using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsPortal3.Domain.ErrorHandler;
using NewsPortal3.Domain.Services.Interfaces;
using NewsPortal3.Models;
using NewsPortal3.Models.Auxiliary;
using NewsPortal3.Models.Identity;
using NewsPortal3.Models.ViewModels;
using ProblemDetails = NewsPortal3.Domain.ErrorHandler.ProblemDetails;

namespace NewsPortal3.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/account")]
    public class AccountController : Controller
    {
        private readonly IUserService _service;
        private readonly SignInManager<User> _signManager;

        public AccountController(IUserService service, SignInManager<User> signManager)
        {
            _service = service;
            _signManager = signManager;
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="user"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <response code="200">return Success</response>
        [HttpPost("register")]
        [ProducesResponseType(200)]
        public async Task<AnswerModel> Register(UserViewModel user, string email)
        {
            user.Roles = null;
            var answer = await _service.AddUser(user, email);
            if (answer.ProblemDetails != null)
            {
                return answer;
            }
            var _user = answer.Data as User;
            await _signManager.SignInAsync(_user, false);
            return new AnswerModel(Constants.Answers.Success);
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <response code="200">returns Success</response>
        [HttpPost("login")]        
        [ProducesResponseType(200)]
        public async Task<AnswerModel> Login(string login, string password)
        {
            var result = await _signManager.PasswordSignInAsync(login, password, false, false);
            if (result.Succeeded)
            {
                return new AnswerModel(Constants.Answers.Success);
            }
            return new AnswerModel(new ProblemDetails
            {
                Title = Constants.Errors.Auth,
                Status = 400,
                Detail = "Provided login or password are incorrect"
            });
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        /// <response code="200">returns Success</response>
        [HttpPost("logout")]
        [ProducesResponseType(200)]
        public async Task<AnswerModel> Logout()
        {
            await _signManager.SignOutAsync();
            return new AnswerModel(Constants.Answers.Success);
        }
    }
}