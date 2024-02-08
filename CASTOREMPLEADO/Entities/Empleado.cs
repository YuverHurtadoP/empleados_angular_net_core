using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CASTOREMPLEADO.Entity;

public partial class Empleado
{
    public int Id { get; set; }

    public int Cedula { get; set; }

    public string Nombre { get; set; } = null!;

    public string Foto { get; set; } = null!;

    public DateOnly FechaIngreso { get; set; }
 
    public int CargoId { get; set; }

    public virtual Cargo? Cargo { get; set; } = null!;
}
