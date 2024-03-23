using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiAutos.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace WebApiAutos.Controllers
{
    [ApiController]
    [Route("Autos")]
    public class AutosController : ControllerBase
    {
        private readonly List<Autos> _autos; 

        public AutosController()
        {
            _autos = new List<Autos>
            {
                new Autos {Id = 0, Marca = "Toyota", Modelo = "Corolla", Year = 2022, Caballos = 150, VelocidadMaxima = 200 },
                new Autos {Id = 1,  Marca = "Ford", Modelo = "Mustang", Year = 2021, Caballos = 300, VelocidadMaxima = 250 }
            };
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult GetAll()
        {
            return Ok(_autos);
        }

        [HttpGet("{id}")]
        [Route("Get")]
        public IActionResult GetById(int id)
        {
            var auto = _autos.Find(a => a.Id == id);
            if (auto == null)
            {
                return NotFound();
            }
            return Ok(auto);
        }

        [HttpPost]
        [Route("Post")]
        public IActionResult AddAuto(Autos auto)
        {
            _autos.Add(auto);
            return CreatedAtAction(nameof(GetById), new { id = auto.Id }, auto);
        }

        [HttpPut("{id}")]
        [Route("Put")]
        public IActionResult UpdateAuto(int id, Autos updatedAuto)
        {
            var existingAuto = _autos.Find(a => a.Id == id);
            if (existingAuto == null)
            {
                return NotFound();
            }

            existingAuto.Marca = updatedAuto.Marca;
            existingAuto.Modelo = updatedAuto.Modelo;
            existingAuto.Year = updatedAuto.Year;
            existingAuto.Caballos = updatedAuto.Caballos;
            existingAuto.VelocidadMaxima = updatedAuto.VelocidadMaxima;

            return Ok(existingAuto);
        }

        [HttpPatch("{id}")]
        [Route("Patch")]
        public IActionResult PartiallyUpdateAuto(int id, [FromBody] JsonPatchDocument<Autos> patchDoc)
        {
            var existingAuto = _autos.Find(a => a.Id == id);
            if (existingAuto == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(existingAuto);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(existingAuto);
        }


        [HttpDelete("{id}")]
        [Route("Delete")]
        public IActionResult DeleteAuto(int id)
        {
            var existingAuto = _autos.Find(a => a.Id == id);
            if (existingAuto == null)
            {
                return NotFound();
            }

            _autos.Remove(existingAuto);

            return NoContent();
        }

    }
}
