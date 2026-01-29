using ProyectoConsolaV2.Models;
using Spectre.Console;

namespace ProyectoConsolaV2.Controlador
{
    internal class ExamenesController
    {
        
        List<Examan> listaExamenes;
        DbConexion db;
        public ExamenesController() {
            
            db = new DbConexion();
            listaExamenes = db.listadoExamenes();
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

        internal List<string> listarExamenesString()
        {
            List<string> listado = new List<string>();

            foreach (Examan e in listaExamenes)
            {
                listado.Add($"{e.Id}- {e.Nombre}");
            }

            return listado;
        }
    }
}
