// --------------------------------------------------------------
// Project: DreamTravelWebAPI
// Class: UserUpdateDTO
// Author: Wijesooriya W.M.R.K
// Created: 10/13/2023
// Description: Model for userupdate in the Dream Travel Web API
// --------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace DreamTravelWebAPI.Models
{
    public class UserUpdateDTO
    {
        [StringLength(100, ErrorMessage = "Password length should be between 6 and 100 characters.", MinimumLength = 6)]
        public string Password { get; set; }

    }

}
