using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BehaviourManagementSystem_MVC.Models
{
    public class ImageModel
    {
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
    }
}
