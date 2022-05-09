namespace LibraryMvcApp.Models
{
    public class UserModel
    {
        private int _userId;
        private string _userName;
        private string _password;
        private bool _isAdmin;

        public int UserId { get => _userId; set => _userId = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public string Password { get => _password; set => _password = value; }
        public bool IsAdmin { get => _isAdmin; set => _isAdmin = value; }
    }
}
