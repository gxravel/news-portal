using NewsPortal3.Domain.ErrorHandler;
using NewsPortal3.Models.Identity;
using NewsPortal3.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal3.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<AnswerModel> GetUsers(int? page, int? pageSize);
        Task<AnswerModel> AddUser(UserViewModel user, string email);
        Task<AnswerModel> EditUser(UserViewModel user);
        Task<AnswerModel> DeleteUser(string login);
    }
}
