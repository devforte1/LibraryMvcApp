using System;

namespace LibraryMvcApp.Models
{
    public class RegistrantModel
    {
        private string _userName;
        private string _password;
        
        // public int Id { get; set; }
        public string UserName { get => _userName; set => _userName = value; }
        public string Password { get => _password;  set => _password = value; }
    }
}
