using Microsoft.AspNetCore.Identity;
using NewsPortal3.Models.Auxiliary;
using NewsPortal3.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NewsPortal3.Models.Identity
{
    public class User : IdentityUser<Guid>
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public void AssignUser(UserViewModel user)
        {
            UserName = user.Login ?? UserName;
            LastName = user.LastName ?? LastName;
            FirstName = user.FirstName ?? FirstName;
            MiddleName = user.MiddleName ?? MiddleName;
            BirthDate = user.BirthDate ?? BirthDate;
        }
        public UserViewModel GetUserViewModel()
        {
            var viewModel = new UserViewModel();
            viewModel.AssignUser(this);
            return viewModel;
        }
    }
}
