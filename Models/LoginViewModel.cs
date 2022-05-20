using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryMvcApp.Models
{
    public class LoginViewModel
    {
        private int _userId;
        private string _userName;
        private string _password;
        private bool _isAdmin;
        private string _roleName;
        private string _firstName;
        private string _lastName;
        private string _email;


        [Required(ErrorMessage = "UserName is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "UserName should be between 6 and 50 characters")]
        public string UserName { get => _userName; set => _userName = value; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password should be between 6 and 50 characters")]
        [DataType(DataType.Password)]
        public string Password { get => _password; set => _password = value; }
    }
}
