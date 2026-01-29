using ProyectoConsolaV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoConsolaV2.Controlador
{
    internal class PacienteController
    {
        DbConexion db;
        public PacienteController()
        {
            db = new DbConexion();
        }

        public List<Paciente> recuperarPacientes()
        {
            return db.listadoPaciente();
        }
    }
}
