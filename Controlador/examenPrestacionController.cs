using ProyectoConsolaV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoConsolaV2.Controlador
{
    internal class examenPrestacionController
    {
        List<ExamenPrestacion> listaExamenPrestacion;
        DbConexion db;

        public examenPrestacionController()
        {
            db = new DbConexion();
        }

        public bool existeExamenPrestacion(int idp, int ide)
        {
            listaExamenPrestacion = db.listadoExPrest();

            if( listaExamenPrestacion.FirstOrDefault(x => x.IdExamen == ide && x.IdPrestacion == idp) == null)
            {
                return false;
            }
            return true;
        }

        internal bool agregarExamenPrestacion(int idPrestacion, int idExamen)
        {
            ExamenPrestacion examenPrestacion = new ExamenPrestacion();
            examenPrestacion.IdPrestacion = idPrestacion;  
            examenPrestacion.IdExamen = idExamen;
            return db.agregarExamenPrestacion(examenPrestacion);
        }
    }
}
