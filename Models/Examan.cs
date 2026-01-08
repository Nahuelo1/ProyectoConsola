using System;
using System.Collections.Generic;

namespace ProyectoConsolaV2.Models;

public partial class Examan
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public bool? Cerrado { get; set; }

    public bool? Baja { get; set; }

    public string? Observacion { get; set; }

    public virtual ICollection<ExamenPrestacion> ExamenPrestacions { get; set; } = new List<ExamenPrestacion>();
}
