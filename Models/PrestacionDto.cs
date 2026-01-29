using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoConsolaV2.Models
{
    public class PrestacionDto
    {
        public Prestacion Prestacion { get; set; }

        public string? NombreEmpresa { get; set; }

        public string? NombrePaciente { get; set; }

        public List<Examan> ListadoExamen {  get; set; }

        
    }
}
