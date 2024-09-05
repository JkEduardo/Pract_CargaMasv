namespace Modelos
{
    public class Usuario
    {       
        public int IdBa { get; set; }
        public string Nombre { get; set; }
        public string?  ApellidoP { get; set; }
        public string? ApellidoM { get; set; }

        public string Sexo { get; set; }
        public string? FechaNacimeinto { get; set; }

        public string? UsuarioBa { get; set; }

        public string Email { get; set; }

        public string? ClaveUs { get; set; }

        public string? Domicilio { get; set; }

        public string? CodigoP { get; set; }

        public string? Localidad { get; set; }

        public string Estado { get; set; }

        public string? Pais { get; set; }

        public string? Puesto { get; set; }

        public int ModAdm { get; set; }

        public int ModAtencion { get; set; }

        public int ModVentas { get; set; }

        public int ModCompras { get; set; }

        public int ModHistorico { get; set; }

        public bool StatusUsuario { get; set; }
        public string? FechaInicio { get; set; }
        public string? FechaBaja { get; set; }

        public string? Imagen { get; set; } 
        public double w { get; set; }
        public double y { get; set; }
        public double vw { get; set; }
        public double vy { get; set; }
        public string? Video { get; set; }
        public string? ImagenFormato { get; set; }
        public string? VideoFormato { get; set; }
        public double Videosize { get; set; }
        public List<object> ListUsuarios { get; set; }
    }
}
