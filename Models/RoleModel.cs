using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryMvcApp.Models
{
    public class RoleModel
    {
        private int _roleId;
        private string _name;

        [Required(ErrorMessage = "RoleId is required.")]
        public int RoleId { get => _roleId; set => _roleId = value; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get => _name; set => _name = value; }
    }
}
