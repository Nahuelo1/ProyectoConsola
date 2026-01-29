using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProyectoConsolaV2.Controlador;
using ProyectoConsolaV2.Models;
using Spectre.Console;
using System.Globalization;

namespace ProyectoConsolaV2.Views
{
    internal class Menu
    {
        PrestacionesController prestaciones;
        ExamenesController examenes;
        examenPrestacionController examenPrestacion;
        DbConexion db;
        
        public Menu()
        {
            prestaciones = new PrestacionesController();
            examenes = new ExamenesController();
            examenPrestacion = new examenPrestacionController();    
            db = new DbConexion();
        }

        public void iniciarApp()
        {
            string eleccion;

            do
            {

                eleccion = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("[Green]Elija una opción[/]")
                        .AddChoices(
                            "Listar",
                            "Crear Prestación",
                            "Agregar Examen a Prestación",
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
                    case "Agregar Examen a Prestación":
                        agregarExamenPrestacion();
                        break;
                    case "Eliminar Prestación":
                        eliminarPrestacion();
                        break;
                    case "Finalizar":
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
                            new SelectionPrompt<string>()
                            .Title("[Green]Elija una opción[/]")
                            .AddChoices(
                                "Listar Prestaciones",
                                "Listar Examenes",
                                "Listar Todo",
                                "Volver"
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
            } while (eleccion != "Volver");
        
        }

        public void agregarExamenPrestacion()
        {
            string prestacionString;
            string examenString;
            int idPrestacion,idExamen;
            
            List<string> listadoPrestaciones = prestaciones.obtenerPrestacioneString();
            List<string> listaExamenes = examenes.listarExamenesString();
            do
            {
                prestacionString = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("[Green]Elija la prestación para agregar examen[/]")
                        .AddChoices(listadoPrestaciones)
                );

                idPrestacion = int.Parse(prestacionString.Split("-")[0].Trim());

                Prestacion prestacion = prestaciones.obtenerPrestacion(idPrestacion);

                if (prestacion.Cerrado == true)
                {
                    AnsiConsole.MarkupLine("[Red] Esta prestación esta cerrada, para editarla vuelva a abrirla [/] ");
                }
                else
                {
                    examenString = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("[Green]Elija la prestación para agregar examen[/]")
                        .AddChoices(listaExamenes)
                     );

                    idExamen = int.Parse(examenString.Split("-")[0].Trim());

                    if(examenPrestacion.existeExamenPrestacion(idPrestacion,idExamen))
                        AnsiConsole.MarkupLine("[Red] El examen ya esta agregado a la prestación [/] ");
                    else
                    {
                        if(examenPrestacion.agregarExamenPrestacion(idPrestacion,idExamen))
                            AnsiConsole.MarkupLine("[Green] El examen fue agregado a la prestación correctamente [/] ");

                    }


                    if (!AnsiConsole.Confirm("¿Desea seguir agregando examen?"))
                        break;

                }
            } while (true);
            
        }

        public void crearPrestacion()
        {
            string tipo;
            DateOnly fecha;
            bool creado = false;

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

                creado = prestaciones.crear(tipo, fecha, idEmpresa,idPaciente);

                if (creado)
                    AnsiConsole.MarkupLine("[Green] La Prestación se ha creado correctamente [/]");
                else
                    AnsiConsole.MarkupLine("[Red] La Prestación no se ha podido crear [/]");

                if(!AnsiConsole.Confirm("Desea continuar ?"))
                    break;

            } while (true);

           

        }

        public void eliminarPrestacion()
        {
            string prestacionString;
            int idPrestacion;

            List<string> listadoPrestaciones = prestaciones.obtenerPrestacioneString();
            do
            {
                prestacionString = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("[Green]Elija la prestación para agregar examen[/]")
                        .AddChoices(listadoPrestaciones)
                );

                idPrestacion = int.Parse(prestacionString.Split("-")[0].Trim());
                
                if(prestaciones.darBaja(idPrestacion))
                    AnsiConsole.MarkupLine("[Green] La prestación se elimino correctamente [/] ");

                if (!AnsiConsole.Confirm("¿Desea seguir eliminado prestaciones?"))
                    break;

            } while(true);
        }
    }
}
