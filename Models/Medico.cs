using System;
using System.Collections.Generic;

namespace ProyectoConsolaV2.Models;

public partial class Medico
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Especialidad { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? Dni { get; set; }

    public virtual ICollection<ExamenPrestacion> ExamenPrestacions { get; set; } = new List<ExamenPrestacion>();
}
