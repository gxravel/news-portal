using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsPortal3.Domain.ErrorHandler;
using NewsPortal3.Domain.Services.Interfaces;
using NewsPortal3.Models.Auxiliary;
using NewsPortal3.Models.ViewModels;

namespace NewsPortal3.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [ApiController]
    [Route("/users")]
    [Authorize(Roles = Constants.Roles.Administrator)]
    public class UserManagerController : Controller
    {
        private readonly IUserService _service;

        public UserManagerController(IUserService service)
        {
            _service = service;
        }
        /// <summary>
        /// Get users
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the list of users</response>
        [HttpGet("get")]
        [ProducesResponseType(200)]
        public async Task<AnswerModel> Get(int? page, int? pageSize)
        {
            return await _service.GetUsers(page, pageSize);
        }

        /// <summary>
        /// Edit the user
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the answer if user was edited</response>
        [HttpPost("edit")]
        [ProducesResponseType(200)]
        public async Task<AnswerModel> Edit(UserViewModel user)
        {
            return await _service.EditUser(user);
        }

        /// <summary>
        /// Add the user
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the answer if user was added</response>
        [HttpPost("add")]
        [ProducesResponseType(200)]
        public async Task<AnswerModel> Add(UserViewModel user, string email)
        {
            return await _service.AddUser(user, email);
        }

        /// <summary>
        /// Delete the user
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the answer if user was deleted</response>
        [HttpPost("delete")]
        [ProducesResponseType(200)]
        public async Task<AnswerModel> Delete(string login)
        {
            return await _service.DeleteUser(login);
        }
    }
}