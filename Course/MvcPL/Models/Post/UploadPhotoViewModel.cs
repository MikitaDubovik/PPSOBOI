using System.Web;

namespace MvcPL.Models
{
    public class UploadPostViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}