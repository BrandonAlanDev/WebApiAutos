using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebApiAutos.Models
{
    public class Autos
    {
        public int Id{get; set;}
        public string? Marca{get; set;}
        public string? Modelo{get; set;}
        public int Year{get;set;}
        public double Caballos{get; set;}//Caballos de fuerza
        public double VelocidadMaxima{get;set;}
    }
}