
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProyectoConsolaV2.Models;
using Spectre.Console;
using System.Drawing;

namespace ProyectoConsolaV2.Controlador

{
    internal class PrestacionesController
    {

        Dictionary<int, Prestacion> prestacionesDiccionario;
        List<Prestacion> listadoPrestaciones;
        List<PrestacionDto> listadoTotalPrestaciones;

        //Conexión segura de la bd
        DbConexion db;

        public PrestacionesController()
        {
            prestacionesDiccionario = new Dictionary<int, Prestacion>();
            db = new DbConexion();
        }

        public void listarPrestaciones()
        {
            try
            {
                listadoPrestaciones = db.obtenerPrestaciones();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[Red]Error al conectar a la DB - Verifique su VPN [/]");
            }


            var tablaPrestaciones = new Table()
                .Border(TableBorder.Rounded)
                .BorderColor(Spectre.Console.Color.Blue)
                .AddColumns("Nro Prestación", "Tipo", "Cerrado", "Enviado");

            foreach (var p in listadoPrestaciones)
            {
                tablaPrestaciones.AddRow(p.NroPrestacion.ToString(), p.Tipo.ToString(), p.Cerrado == false ? "Abierto" : "Cerrado", p.EEnviado == false ? "Sin Enviar" : "Enviado");
            }
            AnsiConsole.Write(tablaPrestaciones);

        }

        public void listarTodo()
        {
            listadoTotalPrestaciones = db.listadoTotalPrestaciones();
            PrestacionDto p;
            var raiz = new Tree("[Yellow] INFORMACIÓN [/]")
                .Style(Style.Parse("blue bold"));

            
            for(int i = 0; i<listadoTotalPrestaciones.Count(); i++) 
            {
                                
                
                p = listadoTotalPrestaciones[i];
                
                //Generamos tabla para mostrar paciente y empresa
                var table = new Table().BorderColor(Spectre.Console.Color.Aqua);
                table.AddColumns("[Yellow] Paciente [/]", "[Yellow] Empresa [/]");
                table.AddRow(p.NombrePaciente, p.NombreEmpresa);
                

                var raizP = raiz.AddNode(
                    $" [Green]Prestación: [/] {p.Prestacion.NroPrestacion.ToString() }  -  {p.Prestacion.Tipo.ToString()} - Estado: {(p.Prestacion.Cerrado == false ? "Abierto" : "Cerrado")} - Enviado: {(p.Prestacion.EEnviado == false ? "Sin Enviar" : "Enviado")}"
                );

                //Agregamos la información de la tabla como un nodo
                var nodoHijoPrest = raizP.AddNode(
                    table
                );

                //Generamos tabla para mostrar Exameens de una prestación
                var tableExamen = new Table().BorderColor(Spectre.Console.Color.Green);
                tableExamen.AddColumns("[Yellow] Examen [/]", "[Yellow]Estado[/]" ,"[Yellow] Observación [/]");

                //Recorremos de cada prestación los examenes 
                foreach (Examan ex in p.ListadoExamen)
                {   
                    tableExamen.AddRow(ex.Nombre, ex.Cerrado == true ? "Abierto" : "Cerrado", ex.Observacion);
                    nodoHijoPrest.AddNode(tableExamen);

                }
                    
               
            }


            AnsiConsole.Write(raiz);
        }

        public void crear()
        {

        }
    }


}
