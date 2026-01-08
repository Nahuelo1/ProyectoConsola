using System;
using System.Collections.Generic;

namespace ProyectoConsolaV2.Models;

public partial class Empresa
{
    public int Id { get; set; }

    public string? RazonSocial { get; set; }

    public virtual ICollection<Prestacion> Prestacions { get; set; } = new List<Prestacion>();
}
