using ProyectoConsolaV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoConsolaV2.Controlador
{
    internal class BdController
    {
        ProjectConsoleContext context;
        public BdController()
        {
            context =  new ProjectConsoleContext();
        }

        public List<Prestacion> recuperarListadoPrestacion()
        {
            List<Prestacion> prestaciones = new List<Prestacion>();
            prestaciones = context.Prestacions.ToList();
            return prestaciones;
        }
    }
}
