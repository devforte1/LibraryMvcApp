namespace LibraryMvcApp.Models
{
    public class MediaModel
    {
        private int _mediaId;
        private string _mediaType;
        private string _name;
        private int _quantity;

        public int MediaId { get => _mediaId; set => _mediaId = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        public string Type { get { return _mediaType; } set { _mediaType = value; } }
        public string Name { get { return _name; } set { _name = value; } }
    }
}
