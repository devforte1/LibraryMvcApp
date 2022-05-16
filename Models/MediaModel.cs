using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryMvcApp.Models
{
    public class MediaModel
    {
        private int _mediaId;
        private string _mediaType;
        private string _name;
        private int _quantity;

        //public MediaModel(int mediaId, int quantity, string mediaType, string name)
        //{
        //    _mediaId = mediaId;
        //    _quantity = quantity;
        //    _mediaType = mediaType;
        //    _name = name;
        //}


        /*
            [Required(ErrorMessage = "MediaId is required.")]
        */
        public int MediaId { get => _mediaId; set => _mediaId = value; }

        [Required(ErrorMessage ="Quantity is required.")]
        public int Quantity { get => _quantity; set => _quantity = value; }

        [Required(ErrorMessage ="Type is required.")]
        public string Type { get { return _mediaType; } set { _mediaType = value; } }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get { return _name; } set { _name = value; } }
    }
}
