using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProyectoConsolaV2.Models;
using Spectre.Console;
using System.Globalization;

namespace ProyectoConsolaV2.Controlador
{
    internal class Menu
    {
        PrestacionesController prestaciones;
        ExamenesController examenes;
        DbConexion db;
        
        public Menu()
        {
            prestaciones = new PrestacionesController();
            examenes = new ExamenesController();
            db = new DbConexion();
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
                            "Editar Pretación",
                            "Eliminar Prestación",
                            "Finalizar"
                        )
                    );

                switch (eleccion)
                {

                    case "Listar":
                        listado();
                        break;
                    case "Crear Prestación":
                        crearPrestacion();
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
                                "Listar Examenes",
                                "Listar Todo",
                                "Finalizar"
                            )
                        );

                switch (eleccion)
                {
                    case "Listar Prestaciones":
                        prestaciones.listarPrestaciones();
                        break;

                    case "Listar Examenes":
                        examenes.listarExamenes();
                        break;
                    case "Listar Todo":
                        prestaciones.listarTodo();
                        break;
                }
            } while (eleccion != "Finalizar");
        
        }

        public void modificarObjeto()
        {
            
        }

        public void crearPrestacion()
        {
            string tipo;
            DateOnly fecha;
            
            string paciente,empresa;
            int idPaciente, idEmpresa;
        
            List<Paciente> pacientes = db.listadoPaciente();
            List<string> pacienteOption = new List<string>();

            List<Empresa> empresas = db.listadoEmpresas();  
            List<string> empresaOption = new List<string> ();

            foreach (var item in pacientes)
            {
                pacienteOption.Add($"{item.Id}- {item.Nombre} {item.Apellido}");
            }

            foreach (var item in empresas)
            {
                empresaOption.Add($"{item.Id}- {item.RazonSocial}");
            }
            
            do
            {
                var tipoPrompt = new TextPrompt<string>("Ingrese el tipo").Validate(tipoPrompt => !tipoPrompt.IsNullOrEmpty(),"[Red]Ingrese un valor[/]");
                tipo = AnsiConsole.Prompt(tipoPrompt);

                //Convertimos la fecha de string a formato dd/mm/aaaa
                var fechaPrompt = new TextPrompt<string>("Ingrese la fecha DD/MM/AAAA")
                    .ValidationErrorMessage("[red]Formato inválido. Use DD/MM/AAAA[/]")
                    .Validate(fechaPrompt =>
                        DateOnly.TryParseExact(
                            fechaPrompt,
                            "dd/MM/yyyy",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out _)
                    );
                string fechaText = AnsiConsole.Prompt(fechaPrompt);
                fecha = DateOnly.ParseExact(
                        fechaText,
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture
                        );


                //Muestro al paciente en formato texto y luego recupero su id
                paciente = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("Elija un paciente")
                        .AddChoices(pacienteOption)
                
                );

                empresa = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Elija una empresa")
                    .AddChoices(empresaOption)
                );


                empresa = empresa.Split("-")[0].Trim();
                paciente = paciente.Split("-")[0].Trim();

                idEmpresa = int.Parse(empresa);
                idPaciente = int.Parse(paciente);

                AnsiConsole.WriteLine(idPaciente + "<Empresa --- Paciente> " + idEmpresa);
                prestaciones.crear();
            } while (true);

           

        }

        public void sacarObjeto()
        {
            
        }
}
}
