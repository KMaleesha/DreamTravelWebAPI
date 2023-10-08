using System.ComponentModel.DataAnnotations;

namespace DreamTravelWebAPI.Models
{
    public class UserUpdateDTO
    {
        [StringLength(100, ErrorMessage = "Password length should be between 6 and 100 characters.", MinimumLength = 6)]
        public string Password { get; set; }

    }

}
