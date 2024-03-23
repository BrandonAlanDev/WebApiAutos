using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiAutos.Models;

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
                new Autos { Marca = "Toyota", Modelo = "Corolla", Year = 2022, Caballos = 150, VelocidadMaxima = 200 },
                new Autos { Marca = "Ford", Modelo = "Mustang", Year = 2021, Caballos = 300, VelocidadMaxima = 250 }
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

    }
}
