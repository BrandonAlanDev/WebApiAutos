using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq.Expressions;
using WebApiAutos.Models;
using Microsoft.AspNetCore.JsonPatch;
using WebApiAutos.Services;

namespace WebApiAutos.Controllers
{
    [ApiController]
    [Route("Autos")]
    public class AutosController : ControllerBase
    {
        private readonly AutosService _autos;
        public AutosController(AutosService autosService)
        {
            _autos = autosService;
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult GetAll()
        {
            return Ok(_autos.GetAllAutos());
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById([FromQuery] int id)
        {
            var auto = _autos.GetAllAutos().Find(a => a.Id == id);
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
                return Ok(_autos.GetAllAutos());
            }
            else
            {
                var autosByMarca = _autos.GetAllAutos().FindAll(a => a.Marca == marca);
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
            _autos.AddAuto(auto); // Llama al método AddAuto del servicio para agregar el auto
            return CreatedAtAction(nameof(GetById), new { id = auto.Id }, auto);
        }

        [HttpPut]
        [Route("Put")]
        public IActionResult UpdateAuto([FromQuery]int id, Autos updatedAuto)
        {
            var existingAuto = _autos.GetAutoById(id); // Obtener el auto existente por su ID

            if (existingAuto == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del auto existente con los valores del auto actualizado
            existingAuto.Marca = updatedAuto.Marca;
            existingAuto.Modelo = updatedAuto.Modelo;
            existingAuto.Year = updatedAuto.Year;
            existingAuto.Caballos = updatedAuto.Caballos;
            existingAuto.VelocidadMaxima = updatedAuto.VelocidadMaxima;

            // Llamar al método de AutosService para actualizar el auto en la lista
            _autos.UpdateAuto(existingAuto);

            return Ok(existingAuto);
        }

        [HttpPatch]
        [Route("Patch")]
        public IActionResult PatchUpdateAuto([FromQuery]int id, Autos updatedAuto)
        {
            var existingAuto = _autos.GetAutoById(id); // Obtener el auto existente por su ID

            if (existingAuto == null)
            {
                return NotFound();
            }
            updatedAuto.Id = id;
            _autos.PatchUpdateAuto(updatedAuto);
            existingAuto = _autos.GetAutoById(id);
            return Ok(existingAuto);
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteAuto(int id)
        {
            var existingAuto = _autos.GetAutoById(id);

            if (existingAuto == null)
            {
                return NotFound();
            }

            _autos.DeleteAuto(existingAuto);

            return NoContent();
        }
    }
}
