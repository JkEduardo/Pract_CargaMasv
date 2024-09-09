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

        public static Modelos.Result GetAllExport(Modelos.Usuario usuario)
        {
            Modelos.Result result = new Modelos.Result();
            try
            {
                using (Conexion.BarberalContext context = new Conexion.BarberalContext())
                {
                    // Ejecuta el Stored Procedure 'GetAllExport'
                    var query = context.UsuariosBas.FromSqlRaw($"GetAllExpor").ToList();

                    // Inicializa la lista para almacenar los resultados
                    result.Objects = new List<object>();

                    if (query != null && query.Count > 0)
                    {
                        foreach (var obj in query)
                        {
                            // Mapear los datos del resultado del Stored Procedure al objeto 'Usuario'
                            usuario = new Modelos.Usuario
                            {
                                StatusUsuario = obj.StatusUsuario.Value,
                                IdBa = obj.IdBa,
                                Nombre = obj.Nombre,
                                ApellidoP = obj.ApellidoP,
                                ApellidoM = obj.ApellidoM,
                                Sexo = obj.Sexo,
                                FechaNacimeinto = obj.FechaNacimeinto.ToString(),
                                UsuarioBa = obj.UsuarioBa,
                                ClaveUs = obj.ClaveUs,
                                Domicilio = obj.Domicilio,
                                CodigoP = obj.CodigoP
                            };

                            // Agregar el objeto 'Usuario' a la lista de resultados
                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se han encontrado registros.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Captura cualquier excepción y establece el resultado como incorrecto
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            // Retorna el resultado final
            return result;
        }

    }
}
