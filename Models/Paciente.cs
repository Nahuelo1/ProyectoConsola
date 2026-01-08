using System;
using System.Collections.Generic;

namespace ProyectoConsolaV2.Models;

public partial class Paciente
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? Dni { get; set; }

    public string? Sexo { get; set; }

    public virtual ICollection<Prestacion> Prestacions { get; set; } = new List<Prestacion>();
}
