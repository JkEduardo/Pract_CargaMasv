using Microsoft.AspNetCore.Mvc;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using ClosedXML.Excel;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data.SqlClient;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Modelos;
using Grpc.Core;
using System.Drawing;
using MediaInfo.DotNetWrapper;
using MediaInfo.DotNetWrapper.Enumerations;


namespace Pract_CargaMasv.Controllers
{
    public class ExportarController : Controller
    {        
        //public ActionResult Exportar()
        //{

        //    Modelos.Usuario usuario = new Modelos.Usuario();
        //    Modelos.Result resultUs = Metodos.Usuario.GetAll(usuario);
        //    if (resultUs.Correct)
        //    {
        //        usuario.ListUsuarios = resultUs.Objects.ToList();
        //        return View(usuario);
        //    }
        //    else
        //    {
        //        return ViewBag("Error en el proceso");
        //    }
        //}

        public ActionResult Exportar(int page = 1)
        {
            const int PageSize = 10;

            Modelos.Usuario usuario = new Modelos.Usuario();
            Modelos.Result resultUs = Metodos.Usuario.GetAll(usuario);

            if (resultUs.Correct)
            {
                var usuarios = resultUs.Objects.Cast<Modelos.Usuario>().ToList();
                var pagedUsuarios = usuarios
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();

                var model = new Modelos.UsuarioViewModel
                {
                    ListUsuarios = pagedUsuarios,
                    CurrentPage = page,
                    TotalPages = (int)Math.Ceiling(usuarios.Count / (double)PageSize)
                };

                return View(model);
            }
            else
            {
                ViewBag.Error = "Error en el proceso";
                return View();
            }
        }


        [HttpPost]
        public ActionResult CargaMasiva()
        {

            string connectionString = "Data Source=RAMOS-CE;Initial Catalog=BARBERAL;Integrated Security=True;";
            string query = "GetAllExport";
            try
            {
                Metodos.CargaMasiva exporter = new Metodos.CargaMasiva();
                string filePath = exporter.ExportToExcel(connectionString, query);
                if (!string.IsNullOrEmpty(filePath))
                {
                    // Abrir el archivo Excel en una nueva pestaña del navegador
                    Response.Headers["Content-Disposition"] = new Microsoft.Extensions.Primitives.StringValues("attachment; filename=ArchivoExportado.xlsx");

                    // Descargar el archivo generado
                    byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                    var result = new
                    {
                        success = true,
                        fileName = Path.GetFileName(filePath),
                        contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        data = Convert.ToBase64String(fileBytes)
                    };
                    return Json(result);

                }
                else
                {
                    // Manejar el caso de error, por ejemplo, redirigir a una página de error
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al exportar datos a Excel: {ex.Message}");
                return RedirectToAction("Error");
            }
        }

        public ActionResult DetalleUsuario(int id)
        {
            Modelos.Usuario usuario = new Modelos.Usuario { IdBa = id };
            Modelos.Result resultUs = Metodos.Usuario.GetById(usuario.IdBa); // Asume que existe un método GetById
            

            if (resultUs.Correct)
            {
                var usuarioDetalle = resultUs.Object as Modelos.Usuario;

                if (!string.IsNullOrEmpty(usuarioDetalle.Imagen))
                {
                    var dimensionesImagen = ObtenerDimensionesImagen(usuarioDetalle.Imagen);
                    usuarioDetalle.w = dimensionesImagen.Width;
                    usuarioDetalle.y = dimensionesImagen.Height;
                    usuarioDetalle.ImagenFormato = Path.GetExtension(usuarioDetalle.Imagen);
                }

                // Asigna el formato del video
                //if (!string.IsNullOrEmpty(usuarioDetalle.Video))
                //{
                //    usuarioDetalle.VideoFormato = Path.GetExtension(usuarioDetalle.Video);
                //}
                if (!string.IsNullOrEmpty(usuarioDetalle.Video))
                {
                    usuarioDetalle.VideoFormato = Path.GetExtension(usuarioDetalle.Video);
                    usuarioDetalle.Videosize = ObtenerTamañoArchivo(usuarioDetalle.Video);
                   
                }

                if (usuarioDetalle != null)
                {
                    return PartialView("_DetalleUsuario", usuarioDetalle); // _DetalleUsuario es la vista parcial
                }
                else
                {
                    return PartialView("_ErrorPartial", "Usuario no encontrado");
                }
            }
            else
            {
                return PartialView("_ErrorPartial", "Error al obtener detalles del usuario");
            }
        }

        private double ObtenerTamañoArchivo(string rutaArchivo)
        {
            // Convertir la ruta relativa a ruta física
            string rutaFisica = rutaArchivo;

            if (System.IO.File.Exists(rutaFisica))
            {
                FileInfo fileInfo = new FileInfo(rutaFisica);
                long sizeInBytes = fileInfo.Length;
                return ConvertirBytesAMegabytes(sizeInBytes);
            }

            return 0; // O cualquier valor para indicar que el archivo no fue encontrado
        } 
        // Método para convertir bytes a megabytes
        private double ConvertirBytesAMegabytes(long bytes)
        {
            return Math.Round((bytes / 1024f) / 1024f, 2);
        }

        private Size ObtenerDimensionesImagen(string rutaArchivo)
        {
            string rutaFisica = rutaArchivo;
            if (System.IO.File.Exists(rutaFisica))
            {
                using (Image image = Image.FromFile(rutaFisica))
                {
                    return new Size(image.Width, image.Height);
                }
            }
            return new Size(0, 0); // O cualquier valor por defecto
        }

    }
}
