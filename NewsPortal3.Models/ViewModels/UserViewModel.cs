using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsPortal3.Models.Auxiliary;
using NewsPortal3.Models.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NewsPortal3.Models.ViewModels
{
    public class UserViewModel
    {
        /// <summary>
        /// Login (username)
        /// </summary>
        [Required]
        public string Login { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        [Required]
        public string LastName { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        public string FirstName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        [Required]
        public string MiddleName { get; set; }
        /// <summary>
        /// Роли
        /// </summary>
        public IList<string> Roles { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public void AssignUser(User user)
        {
            Login = user.UserName ?? Login;
            LastName = user.LastName ?? LastName;
            FirstName = user.FirstName ?? FirstName;
            MiddleName = user.MiddleName ?? MiddleName;
            BirthDate = user.BirthDate ?? BirthDate;
        }
        
    }
}
