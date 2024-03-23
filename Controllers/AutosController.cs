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
        private List<Autos> _autos; 

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

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById([FromQuery] int id)
        {
            var auto = _autos.Find(a => a.Id == id);
            if (auto == null)
            {
                return NotFound();
            }
            return Ok(auto);
        }

        [HttpGet("GetByMarca")]
        public IActionResult GetByMarca([FromQuery] string marca)
        {
            if (string.IsNullOrEmpty(marca))
            {
                return Ok(_autos);
            }
            else
            {
                var autosByMarca = _autos.FindAll(a => a.Marca == marca);
                if (autosByMarca.Count == 0)
                {
                    return NotFound();
                }
                return Ok(autosByMarca);
            }
        }


        [HttpPost]
        [Route("Post")]
        public IActionResult AddAuto(Autos auto)
        {
            _autos.Add(auto);
            return CreatedAtAction(nameof(GetById), new { id = auto.Id }, auto);
        }

        [HttpPut]
        [Route("Put")]
        public IActionResult UpdateAuto([FromQuery] int id, Autos updatedAuto)
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

        [HttpPatch]
        [Route("Patch")]
        public IActionResult PartiallyUpdateAuto([FromQuery] int id, [FromBody] JsonPatchDocument<Autos> patchDoc)
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


        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteAuto([FromQuery] int id)
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
