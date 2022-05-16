using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryMvcApp.Models
{
    public class RegistrantModel
    {
        private string _userName;
        private string _password;
        private string _firstName;
        private string _lastName;
        private string _email;

        // public int Id { get; set; }

        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get => _userName; set => _userName = value; }

        [Required(ErrorMessage ="Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get => _password;  set => _password = value; }

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get => _firstName; set => _firstName = value; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get => _lastName; set => _lastName = value; }

        [Required(ErrorMessage = "Email address is required.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail address is not valid")]
        public string Email { get => _email; set => _email = value; }
    }
}
