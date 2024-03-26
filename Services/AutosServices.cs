using System.Collections.Generic;
using WebApiAutos.Models;

namespace WebApiAutos.Services
{
    public class AutosService
    {
        private List<Autos> _autos;

        public AutosService()
        {
            _autos = new List<Autos>
            {
                new Autos {Id = 0, Marca = "Toyota", Modelo = "Corolla", Year = 2022, Caballos = 150, VelocidadMaxima = 200 },
                new Autos {Id = 1,  Marca = "Ford", Modelo = "Mustang", Year = 2021, Caballos = 300, VelocidadMaxima = 250 }
            };
        }

        public List<Autos> GetAllAutos()
        {
            return _autos;
        }
        public void AddAuto(Autos auto)
        {
            _autos.Add(auto);
        }
        public Autos GetAutoById(int id)
        {
            return _autos.FirstOrDefault(a => a.Id == id);
        }

        public void UpdateAuto(Autos updatedAuto)
        {
            var existingAuto = _autos.FirstOrDefault(a => a.Id == updatedAuto.Id);
            
            if (existingAuto != null)
            {
                existingAuto.Marca = updatedAuto.Marca;
                existingAuto.Modelo = updatedAuto.Modelo;
                existingAuto.Year = updatedAuto.Year;
                existingAuto.Caballos = updatedAuto.Caballos;
                existingAuto.VelocidadMaxima = updatedAuto.VelocidadMaxima;
            }
        }
        public void PatchUpdateAuto(Autos updatedAuto)
        {
            var existingAuto = _autos.FirstOrDefault(a => a.Id == updatedAuto.Id);
            
            if (existingAuto != null)
            {
                if(!string.IsNullOrEmpty(updatedAuto.Marca))existingAuto.Marca = updatedAuto.Marca;
                if(!string.IsNullOrEmpty(updatedAuto.Modelo))existingAuto.Modelo = updatedAuto.Modelo;
                if(updatedAuto.Year!=null && updatedAuto.Year!=0 )existingAuto.Year = updatedAuto.Year;
                if(updatedAuto.Caballos != null && updatedAuto.Caballos!=0 )existingAuto.Caballos = updatedAuto.Caballos;
                if(updatedAuto.VelocidadMaxima!=null && updatedAuto.VelocidadMaxima!=0 )existingAuto.VelocidadMaxima = updatedAuto.VelocidadMaxima;
            }
        }
        public void DeleteAuto(Autos auto)
        {
            _autos.Remove(auto);
        }
    }
}
