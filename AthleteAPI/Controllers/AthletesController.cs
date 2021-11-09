using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AthleteAPI.Interfaces;
using AthleteAPI.Managers;
using AthleteLibrary;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AthleteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AthletesController : ControllerBase
    {
        private IAthletesManager _mgr = new AthletesManager();

        // GET: api/<AthletesController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(_mgr.Get());
        }

        // PUT api/<AthletesController>/5
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] Athlete athlete)
        {
            try
            {
                return Ok(_mgr.Create(athlete));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // DELETE api/<AthletesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(_mgr.Delete(id));
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound(knfe.Message);
            }
        }

        [HttpGet("FilterByName/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult FilterByName(string name)
        {
            return Ok(_mgr.FilterByName(name));
        }

        [HttpGet("FilterByCountry/{country}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult FilterByCountry(string country)
        {
            return Ok(_mgr.FilterByCountry(country));
        }

        [HttpGet("FilterByHeight/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult FilterByHeight([FromQuery]int minHeight, int maxHeight)
        {
            return Ok(_mgr.FilterByHeight(minHeight, maxHeight));
        }
    }
}
