using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProyectoConsolaV2.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoConsolaV2.Controlador
{
    internal class ExamenesController
    {
        
        List<Examan> listaExamenes;
        DbConexion db;
        public ExamenesController() {
            
            db = new DbConexion();
        }


        public void listarExamenes()
        {
            listaExamenes = db.listadoExamenes();
            var tablaExamenes = new Spectre.Console.Table()
                                    .Border(TableBorder.Rounded)
                                    .BorderColor(Color.Green)
                                    .AddColumns("ID","Nombre","Observación");

            foreach(Examan ex in  listaExamenes)
            {
                tablaExamenes.AddRow(ex.Id.ToString(),ex.Nombre.ToString(),ex.Observacion.ToString());
            }

            AnsiConsole.Write(tablaExamenes);
        }
    }
}
