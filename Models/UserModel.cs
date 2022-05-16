using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryMvcApp.Models
{
    public class UserModel
    {
        private int _userId;
        private string _userName;
        private string _password;
        private string _isActive;
        private string _roleName;
        private string _firstName;
        private string _lastName;
        private string _email;


        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get => _userId; set => _userId = value; }

        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get => _userName; set => _userName = value; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get => _password; set => _password = value; }

        [Required(ErrorMessage = "IsAdmin is required.")]
        public string IsActive { get => _isActive; set => _isActive = value; }

        [Required(ErrorMessage = "Role name is required.")]
        public string RoleName { get => _roleName; set => _roleName = value; }

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get => _firstName; set => _firstName = value; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get => _lastName; set => _lastName = value; }

        [Required(ErrorMessage = "Email address is required.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get => _email; set => _email = value; }
    }
}
