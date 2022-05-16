using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryMvcApp.Models
{
    public class UserMediaModel
    {
        
        private int _userId;
        private int _mediaId;
        private string _mediaName;
        private string _mediaType;
        private string _checkOutDate;
        private string _checkInDate;
        private string _holdRequestDate;

        [Display(Name = "UserId")]
        public int UserId { get { return _userId; } set { _userId = value; } }

        [Display(Name = "MediaId")]
        public int MediaId { get { return _mediaId; } set { _mediaId = value;} }

        [Required(ErrorMessage = "MediaName is required.")]
        public string MediaName { get { return _mediaName; } set { _mediaName = value;} }

        [Required(ErrorMessage = "MediaType is required.")]
        public string MediaType { get { return _mediaType; } set { _mediaType = value; } }
        
        public String CheckOutDate { get { return _checkOutDate; } set { _checkOutDate = value; } }
        public String CheckInDate { get { return _checkInDate; } set { _checkInDate = value; } }
        public String HoldRequestDate {  get { return _holdRequestDate; } set { _holdRequestDate = value; } }
    }
}
