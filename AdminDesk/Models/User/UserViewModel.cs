using System;
using System.ComponentModel.DataAnnotations;
using AdminDesk.Entities;
using AdminDesk.Models.ServiceOrder;
using static System.Net.Mime.MediaTypeNames;

namespace AdminDesk.Models.User
{

    public class UserFullViewModel
    {
        public UserFullViewModel()
        {
            UpsertModel = new UserViewModel();

            UserList = new List<UserViewModel>();
        }

        public UserViewModel UpsertModel { get; set; }
        public List<UserViewModel> UserList { get; set; }
    }

    public class UserViewModel
    {
        [Required]
        public string UserId { get; set; }
        public bool Disabled { get; set; }

    }
}