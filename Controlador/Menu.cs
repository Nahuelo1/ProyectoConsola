using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoConsolaV2.Controlador
{
    internal class Menu
    {
        public Menu()
        {
        }
        //Recuperamos la lista de objetos de json apenas empieza la app

        

        public void iniciarApp()
        {
            bool terminar = false;
            string eleccion;

            do
            {

                eleccion = AnsiConsole.Prompt(
                        new SelectionPrompt<String>()
                        .Title("[Green]Elija una opción[/]")
                        .AddChoices(
                            "Ver Objetos de la tienda",
                            "Agregar Objeto",
                            "Sacar Objeto",
                            "Editar Objeto",
                            "Guardar Cambios",
                            "Finalizar"
                        )
                    );

                switch (eleccion)
                {

                    case "Ver Objetos de la ":
                        break;
                    case "Agregar Objeto":
                        break;
                    case "Sacar Objeto":
                        ; break;
                    case "Editar Objeto":
                        break;
                    case "Guardar Cambios":
                        
                        break;
                    case "Finalizar":
                        terminar = true;
                        break;

                    default:
                        AnsiConsole.Clear();
                        AnsiConsole.MarkupLine("[Red] Presione un valor correcto [/]");
                        break;
                }
            } while (!terminar);


        }

        public void modificarObjeto()
        {
            
        }

        public void crearObjeto()
        {
           
        }

        public void sacarObjeto()
        {
            
        }
}
}
