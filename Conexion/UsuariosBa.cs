using System;
using System.Collections.Generic;

namespace Conexion;

public partial class UsuariosBa
{
    public int IdBa { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoP { get; set; }

    public string? ApellidoM { get; set; }

    public string? Sexo { get; set; }

    public DateOnly? FechaNacimeinto { get; set; }

    public string? UsuarioBa { get; set; }

    public string? Email { get; set; }

    public string? ClaveUs { get; set; }

    public string? Domicilio { get; set; }

    public string? CodigoP { get; set; }

    public string? Localidad { get; set; }

    public string? Estado { get; set; }

    public string? Pais { get; set; }

    public string? Puesto { get; set; }

    public int? ModAdm { get; set; }

    public int? ModAtencion { get; set; }

    public int? ModVentas { get; set; }

    public int? ModCompras { get; set; }

    public int? ModHistorico { get; set; }

    public bool? StatusUsuario { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaBaja { get; set; }
}
