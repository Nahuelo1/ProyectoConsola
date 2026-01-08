using System;
using System.Collections.Generic;

namespace ProyectoConsolaV2.Models;

public partial class Prestacion
{
    public int Id { get; set; }

    public int NroPrestacion { get; set; }

    public string? Tipo { get; set; }

    public DateOnly? FechaCreacion { get; set; }

    public bool? Cerrado { get; set; }

    public bool? EEnviado { get; set; }

    public int? IdPaciente { get; set; }

    public int? IdEmpresa { get; set; }

    public int? Baja { get; set; }

    public virtual ICollection<ExamenPrestacion> ExamenPrestacions { get; set; } = new List<ExamenPrestacion>();

    public virtual Empresa? IdEmpresaNavigation { get; set; }

    public virtual Paciente? IdPacienteNavigation { get; set; }
}
