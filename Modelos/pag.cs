using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class UsuarioViewModel
    {
        public List<Modelos.Usuario> ListUsuarios { get; set; } = new List<Modelos.Usuario>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRegistros { get; set; }
        public int RegistrosPorPagina { get; set; }
        public List<int> RegistrosPorPaginaOpciones { get; set; }
    }



}
