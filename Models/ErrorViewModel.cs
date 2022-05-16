using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryMvcApp.Models
{
    public class ErrorViewModel
    {
        [Required(ErrorMessage = "RequestId is required")]
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
