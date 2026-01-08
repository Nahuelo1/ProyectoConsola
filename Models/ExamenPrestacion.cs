using System;
using System.Collections.Generic;

namespace ProyectoConsolaV2.Models;

public partial class ExamenPrestacion
{
    public int Id { get; set; }

    public int? IdPrestacion { get; set; }

    public int? IdExamen { get; set; }

    public int? IdMedico { get; set; }

    public virtual Examan? IdExamenNavigation { get; set; }

    public virtual Medico? IdMedicoNavigation { get; set; }

    public virtual Prestacion? IdPrestacionNavigation { get; set; }
}
