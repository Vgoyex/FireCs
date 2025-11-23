using System.ComponentModel.DataAnnotations;
using Fire.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Fire.Communication.Requests
{

    public record RegistrarPostRequest(
        [Required] Guid user_id,
        [Required] string title,
        [Required] Privacy privacy,
        string body,
        string description,
        List<string> list_contents,        
        List<IFormFile>? files
        )
    {

    }
    //public class RegistrarPostRequest()
    //{
    //    [Required]
    //    public Guid user_id { get; set; }

    //    [Required]
    //    public string title { get; set; }

    //    [Required]
    //    public Privacy privacy { get; set; }

    //    public string body { get; set; }
    //    public string description { get; set; }

    //    public List<string> list_contents { get; set; }
    //    public List<IFormFile> files { get; set; }
    //}
}
