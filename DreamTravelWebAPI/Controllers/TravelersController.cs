// --------------------------------------------------------------
// Project: DreamTravelWebAPI
// Class: TravelersController
// Author: Wijesooriya W.M.R.K
// Created: 10/13/2023
// Description: Controller for managing travelers in the Dream Travel Web API
// --------------------------------------------------------------

using DreamTravelWebAPI.Models;
using DreamTravelWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace DreamTravelWebAPI.Controllers
{
    // Requires the user to be authenticated, except where explicitly overridden
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TravelersController : ControllerBase
    {   
        private readonly IUserService _userService;
        private readonly ITravelerService _travelerService;
        private readonly IConfiguration _config;
        private readonly JwtSettings _jwtSettings;

        // Constructor
        public TravelersController(IUserService userService, ITravelerService travelerService, IConfiguration config, IOptions<JwtSettings> jwtSettings)
        {
            _userService = userService;
            _travelerService = travelerService;
            _config = config;
            _jwtSettings = jwtSettings.Value;
        }

        // Allows anonymous access for registration
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] Traveler traveler)
        {
            if (_travelerService.Exists(traveler.NIC))
                return BadRequest("Traveler already exists");

            var user = _userService.GetByNic(traveler.NIC);
            if (user == null)
                return BadRequest("User account with NIC doesn't exist. Please create a user account before attempting to register traveler details.");

            if (!user.IsActive)
                return BadRequest("User account is deactivated. Please activate the user account before attempting to register traveler details.");

            traveler.Id = null;
            _travelerService.Create(traveler);
            return Ok("Traveler successfully registered");
        }

        // Updates an existing traveler by NIC
        [HttpPut("{nic}")]
        public IActionResult UpdateTraveler(string nic, [FromBody] Traveler travelerUpdateParam)
        {
            var existingTraveler = _travelerService.GetByNic(nic);
            if (existingTraveler == null)
                return NotFound();

            // Update only the required fields
            existingTraveler.Name = travelerUpdateParam.Name;
            existingTraveler.Email = travelerUpdateParam.Email;
            existingTraveler.DateOfBirth = travelerUpdateParam.DateOfBirth;

            _travelerService.Update(nic, existingTraveler);
            return Ok("Traveler updated successfully");
        }

        // Retrieves all travelers
        [HttpGet]
        public IActionResult GetAllTravelers()
        {
            var travelers = _travelerService.GetAll();
            return Ok(travelers);
        }


        // Get traveler details
        [HttpGet("{nic}")]
        public IActionResult GetTravelerDetails(string nic)
        {
            var traveler = _travelerService.GetByNic(nic);
            if (traveler == null)
                return NotFound();
            return Ok(traveler);
        }


        // Delete a traveler
        [HttpDelete("{nic}")]
        public IActionResult DeleteTraveler(string nic)
        {
            if (_travelerService.GetByNic(nic) == null)
                return NotFound();

            _travelerService.Delete(nic);
            return Ok("Traveler deleted successfully");
        }

        // Activate a traveler
        [HttpPatch("{nic}/activate")]
        public IActionResult ActivateTraveler(string nic)
        {
            try
            {
                _travelerService.Activate(nic);
                return Ok("Traveler activated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Deactivate a traveler
        [HttpPatch("{nic}/deactivate")]
        public IActionResult DeactivateTraveler(string nic)
        {
            try
            {
                _travelerService.Deactivate(nic);
                return Ok("Traveler deactivated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
