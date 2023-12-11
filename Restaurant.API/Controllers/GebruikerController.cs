using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Mappers;
using Restaurant.API.Models.Input;
using Restaurant.API.Models.Output;
using Restaurant.BL.Managers;
using Restaurant.BL.Models;
using System;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GebruikerController : ControllerBase
    {
        private GebruikerManager _gebruikerManager;

        public GebruikerController(GebruikerManager gebruikerManager)
        {
            _gebruikerManager = gebruikerManager;
        }

        [HttpGet]
        public ActionResult<List<Gebruiker>> GetAllGebruikers()
        {
            try
            {
                List<Gebruiker> gebruikers = _gebruikerManager.GetGebruikers();
                return Ok(gebruikers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("by-klantNummer/{klantNummer}")]
        public ActionResult<Gebruiker> GetGebruikerByKlantNummer(int klantNummer)
        {
            try
            {
                Gebruiker gebruiker = _gebruikerManager.GetGebruikersByKlantNr(klantNummer);
                return Ok(gebruiker);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("by-email/{email}")]
        public ActionResult<Gebruiker> GetGebruikerByEmail(string email)
        {
            try
            {
                Gebruiker gebruiker = _gebruikerManager.GetGebruikersByEmail(email);
                return Ok(gebruiker);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<GebruikerRESToutputDTO> AddGebruiker([FromBody] GebruikerRESTinputDTO inputDTO)
        {
            try
            {
                var locatie = new Locatie(inputDTO.locatie.Postcode, inputDTO.locatie.Gemeentenaam, inputDTO.locatie.Straatnaam, inputDTO.locatie.Huisnummerlabel);
                var gebruiker = new Gebruiker(inputDTO.Naam, inputDTO.Email, inputDTO.TelefoonNummer, locatie);

                _gebruikerManager.AddGebruiker(gebruiker);
                return CreatedAtAction(nameof(GetGebruikerByEmail), new { Email = gebruiker.Email }, gebruiker);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost]
        //public ActionResult<GebruikerRESToutputDTO> AddGebruiker([FromBody] GebruikerRESTinputDTO inputDTO)
        //{
        //    try
        //    {
        //        //Locatie locatie = new Locatie(inputDTO.Postcode, inputDTO.GemeenteNaam, inputDTO.StraatNaam, inputDTO.HuisNummerLabel);
        //        //Gebruiker gebruiker = new Gebruiker(inputDTO.Naam, inputDTO.Email, inputDTO.TelefoonNummer, locatie);
        //        //_gebruikerManager.AddGebruiker(gebruiker);
        //        //return CreatedAtAction(nameof(GetGebruikerByKlantNummer), new { KlantenNr = gebruiker.KlantenNr }, inputDTO);
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
