using System.ComponentModel.DataAnnotations;
using Fire.Domain.Enums;

namespace Fire.Communication.Requests
{
    public record RegisterUserRequest(
        [Required] string name,
        [Required] string email,
        [Required] string password,
        Role role,
        string nickname
     )
    { }

}
