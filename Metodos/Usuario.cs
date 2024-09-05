using Microsoft.EntityFrameworkCore;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metodos
{
    public class Usuario
    {

        public static Modelos.Result GetAll(Modelos.Usuario usuario)
        {
            Modelos.Result result = new Modelos.Result();
            try
            {
                using (Conexion.BarberalContext context = new Conexion.BarberalContext())
                {
                    var query = context.UsuariosBas.FromSqlRaw($"GetAllExport").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {

                            usuario = new Modelos.Usuario();
                            usuario.StatusUsuario = obj.StatusUsuario.Value;
                            usuario.IdBa = obj.IdBa;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoP = obj.ApellidoP;
                            usuario.ApellidoM = obj.ApellidoM;
                            usuario.Sexo = obj.Sexo;
                            usuario.FechaNacimeinto = obj.FechaNacimeinto.ToString();
                            usuario.UsuarioBa = obj.UsuarioBa;
                            usuario.ClaveUs = obj.ClaveUs;
                            usuario.Domicilio = obj.Domicilio;
                            usuario.CodigoP = obj.CodigoP;
                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se han encontrado registrios ";

                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }
            return result;
        }

        public static Modelos.Result GetById(int IdUsuario)
        {
            Modelos.Result result = new Modelos.Result();
            try
            {
                using (Conexion.BarberalContext context = new Conexion.BarberalContext())
                {
                    var query = context.UsuariosBas.FromSqlRaw($"VerDetalle {IdUsuario}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        Modelos.Usuario usuario = new Modelos.Usuario();
                        usuario.StatusUsuario = query.StatusUsuario.Value;
                        usuario.IdBa = query.IdBa;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoP = query.ApellidoP;
                        usuario.ApellidoM = query.ApellidoM;
                        usuario.Sexo = query.Sexo;
                        usuario.FechaNacimeinto = query.FechaNacimeinto.ToString();
                        usuario.UsuarioBa = query.UsuarioBa;
                        usuario.ClaveUs = query.ClaveUs;
                        usuario.Domicilio = query.Domicilio;
                        usuario.CodigoP = query.CodigoP;
                        usuario.Imagen = (@"C:\Users\PC FACTOR WHITE\Downloads\454013685_346330258540085_4013059045522688320_n_11zon.jpg");
                        usuario.Video = (@"C:\Users\PC FACTOR WHITE\Downloads\Docs Laborales 2023\2d0d9750-11fc-4acb-a778-cdcdc902bcfd.mp4");
                        result.Objects.Add(usuario);

                        result.Object = usuario;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = " Ocurrio un error ";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}
