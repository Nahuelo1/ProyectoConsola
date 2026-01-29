using Microsoft.EntityFrameworkCore;
using ProyectoConsolaV2.Models;
using Spectre.Console;
using System;
using System.Linq;

namespace ProyectoConsolaV2.Controlador
{
    internal class DbConexion
    {
        DbContextOptionsBuilder<ProjectConsoleContext> optionsBuilder;
        public DbConexion() {
            optionsBuilder = new DbContextOptionsBuilder<ProjectConsoleContext>();
            optionsBuilder.UseSqlServer("Server=192.168.111.9; DataBase=ProjectConsole; User=sa; Password=Sapass1?; TrustServerCertificate=True;");


        }


        public List<Prestacion> obtenerPrestaciones()
        {
            List<Prestacion> listadoPrestacion = new List<Prestacion>();
            IQueryable<PrestacionDto> queryPrestacion;
            using (ProjectConsoleContext context = new ProjectConsoleContext(optionsBuilder.Options))
            {
                listadoPrestacion = context.Prestacions.Where(p => p.Baja == 0).OrderBy(p => p.NroPrestacion).ToList();

            }

            return listadoPrestacion;
        }

        public List<PrestacionDto> listadoTotalPrestaciones()
        {
            List<PrestacionDto> listadoPrestaciones;
            List<Examan> listadoExamen;
            IQueryable<PrestacionDto> queryPrestacion;
            IQueryable<PrestacionDto> queryExamen;
            using (ProjectConsoleContext context = new ProjectConsoleContext(optionsBuilder.Options))
            {
                queryPrestacion = from pre in context.Prestacions
                                  join e in context.Empresas on pre.IdEmpresa equals e.Id into empresa
                                  from e in empresa.DefaultIfEmpty()
                                  join pa in context.Pacientes on pre.IdPaciente equals pa.Id into paciente
                                  from pa in paciente.DefaultIfEmpty()
                                  where pre.Baja == 0
                                  select new PrestacionDto
                                  {
                                      Prestacion = pre,
                                      NombreEmpresa = e.RazonSocial,
                                      NombrePaciente = pa.Nombre + " " + pa.Apellido,
                                      ListadoExamen =
                                      (
                                          from ep in context.ExamenPrestacions
                                          join ex in context.Examen on ep.IdExamen equals ex.Id
                                          where ep.IdPrestacion == pre.Id
                                          select ex
                                      ).ToList()
                                  };





                //Listo las prestaciones, filtrando las duplicadas 
                listadoPrestaciones = queryPrestacion.ToList();

            }

            return listadoPrestaciones;
        }

        public List<Paciente> listadoPaciente()
        {
            List<Paciente> listado = new List<Paciente>();

            using (ProjectConsoleContext context = new ProjectConsoleContext(optionsBuilder.Options))
            {
                listado = context.Pacientes.ToList();
            }


            return listado;
        } 

        public List<Examan> listadoExamenes()
        {
            List<Examan> listaExamenes = new List<Examan>();    
            using (ProjectConsoleContext context = new ProjectConsoleContext(optionsBuilder.Options))
            {
                listaExamenes = context.Examen.OrderBy(e => e.Id).ToList();
            }
            return listaExamenes;
        }

        public List<Empresa> listadoEmpresas()
        {
            List<Empresa> listadoEmpresa = new List<Empresa>();
            using (ProjectConsoleContext context = new ProjectConsoleContext(optionsBuilder.Options))
            {
                listadoEmpresa = context.Empresas.ToList();
            }
            return listadoEmpresa;

        }

        internal bool agregarPrestación(Prestacion p)
        {
            using (ProjectConsoleContext context = new ProjectConsoleContext(optionsBuilder.Options))
            {
                try
                {
                    context.Prestacions.Add(p);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex) {
                    AnsiConsole.MarkupLine("[Red]Error al dar de alta la prestación[/]");
                    return false;
                }
            }
        }

        internal List<ExamenPrestacion> listadoExPrest()
        {
            using (ProjectConsoleContext context = new ProjectConsoleContext(optionsBuilder.Options))
            {
                return context.ExamenPrestacions.ToList();
            }
        }

        internal bool agregarExamenPrestacion(ExamenPrestacion e)
        {
            using (ProjectConsoleContext context = new ProjectConsoleContext(optionsBuilder.Options))
            {
                try
                {
                    context.ExamenPrestacions.Add(e);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine("[Red]Error al dar de alta la prestación[/]");
                    return false;
                }
            }

        }

        public async void bajaPrestacion(int idP)
        {
            using (ProjectConsoleContext context = new ProjectConsoleContext(optionsBuilder.Options))
            {
                Prestacion prest = await context.Prestacions.FirstOrDefaultAsync(p => p.NroPrestacion == idP);
                
                if(prest != null) {
                    prest.Baja = 1;
                    context.SaveChanges();
                    
                }
            }


        }

        internal int totalPrestaciones()
        {
            using (ProjectConsoleContext context = new ProjectConsoleContext(optionsBuilder.Options))
            {
               return context.Prestacions.Count();
            }
        }
    }
}
