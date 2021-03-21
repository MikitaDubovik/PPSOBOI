namespace MvcPL.Models
{
    public class UploadAdViewModel : UploadPostViewModel
    {
        public string Language { get; set; }

        public string Age { get; set; }

        public string Sex { get; set; }

        public string Countries { get; set; }

        public string Price { get; set; }

        public byte[] Photo { get; set; }
    }
}