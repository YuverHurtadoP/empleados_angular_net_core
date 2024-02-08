using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CASTOREMPLEADO.Entity;

public partial class Cargo
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    [JsonIgnore]
    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
