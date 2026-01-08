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

        public void iniciarApp()
        {
            string eleccion;

            do
            {

                eleccion = AnsiConsole.Prompt(
                        new SelectionPrompt<String>()
                        .Title("[Green]Elija una opción[/]")
                        .AddChoices(
                            "Listar",
                            "Crear Prestación",
                            "",
                            "Finalizar"
                        )
                    );

                switch (eleccion)
                {

                    case "Listar":
                        listado();
                        break;
                    case "":
                        break;
                    case "2":
                        ; break;
                    case "3":
                        break;
                    case "4":
                        
                        break;
                    default:
                        AnsiConsole.Clear();
                        AnsiConsole.MarkupLine("[Red] Presione un valor correcto [/]");
                        break;
                }
            } while (eleccion != "Finalizar");


        }

        public void listado()
        {
            string eleccion;

            do
            {
                eleccion = AnsiConsole.Prompt(
                            new SelectionPrompt<String>()
                            .Title("[Green]Elija una opción[/]")
                            .AddChoices(
                                "Listar Prestaciones",
                                "Listar Pacientes",
                                "Listar Examenes",
                                "Listar Itemprestación",
                                "Finalizar"
                            )
                        );

                switch (eleccion)
                {
                    case "Listar Prestaciones":
                        BdController controlador = new BdController();
                        foreach (var e in controlador.recuperarListadoPrestacion())
                            AnsiConsole.WriteLine(e.ToString());
                        break;

                    case "Listar Pacientes": 
                        break;
                    case "Listar Examenes":
                        break;
                    case "Listar Itemprestación":
                        break;
                }
            } while (eleccion != "Finalizar");
        
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
