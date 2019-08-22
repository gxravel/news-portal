using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsPortal3.Data;
using NewsPortal3.Data.Auxiliary;
using NewsPortal3.Domain.ErrorHandler;
using NewsPortal3.Domain.Services.Interfaces;
using NewsPortal3.Models;
using NewsPortal3.Models.Auxiliary;
using NewsPortal3.Models.Identity;
using NewsPortal3.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal3.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<AnswerModel> GetUsers(int? page, int? pageSize)
        {
            int _page = page ?? 1;
            int _pageSize = pageSize ?? Constants.PageSize;
            var users = await _userManager.Users
                .Skip((_page - 1) * _pageSize)
                .Take(_pageSize)
                .ToArrayAsync();
            if (users.Length == 0)
            {
                return new AnswerModel();
            }
            var viewModels = new UserViewModel[users.Length];
            for (int i = 0; i < users.Length; i++)
            {
                viewModels[i] = users[i].GetUserViewModel();
                var roles = await _userManager.GetRolesAsync(users[i]);
                viewModels[i].Roles = roles;
            }
            return new AnswerModel(viewModels);
    
        }
        public async Task<AnswerModel> EditUser(UserViewModel user)
        {
            var answer = await FindUserByName(user.Login);
            if (answer.ProblemDetails == null)
            {
                return answer;
            }
            var _user = answer.Data as User;
            _user.AssignUser(user);
            answer = await AddRolesToUser(_user, user.Roles);
            if (answer.ProblemDetails != null)
            {
                return answer;
            }
            await _userManager.UpdateAsync(_user);
            return new AnswerModel();
        }
        public async Task<AnswerModel> AddUser(UserViewModel user, string email)
        {
            var _user = new User
            {
                Email = email,
            };
            _user.AssignUser(user);
            var password = Passwords.New();
            // TODO: Send Email.
            var result = await _userManager.CreateAsync(_user, password);
            if (!result.Succeeded)
            {

            }
            var answer = await AddRolesToUser(_user, user.Roles);
            if (answer.ProblemDetails != null)
            {
                return answer;
            }
            return new AnswerModel(_user);
        }
        public async Task<AnswerModel> DeleteUser(string login)
        {
            var answer = await FindUserByName(login);
            if (answer.ProblemDetails != null)
            {
                return answer;
            }
            await _userManager.DeleteAsync(answer.Data as User);
            return new AnswerModel();
        }
        private async Task<AnswerModel> AddRolesToUser(User user, IList<string> roles)
        {
            var _roles = new List<string>();
            if (roles != null)
            {
                foreach (var r in roles)
                {
                    if (Constants.Roles.All.Contains(r))
                    {
                        _roles.Add(r);
                    }
                }
            }
            if (_roles.Count == 0)
            {
                _roles.Add(Constants.Roles.Reader);
            }
            await _userManager.AddToRolesAsync(user, _roles);
            return new AnswerModel();
        }
        private async Task<AnswerModel> FindUserByName(string userName)
        {
            var _user = await _userManager.FindByNameAsync(userName);
            if (_user == null)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = Constants.Errors.NotFound,
                    Status = 404,
                    Detail = $"User with login '{userName}' doesn't exist."
                };
                return new AnswerModel(problemDetails);
            }
            return new AnswerModel(_user);
        }
    }
}
