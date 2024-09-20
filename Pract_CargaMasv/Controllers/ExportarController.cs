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
using Microsoft.AspNetCore.OutputCaching;
using System.Text.Json;


namespace Pract_CargaMasv.Controllers
{
    public class ExportarController : Controller
    {

        //ApiTokenBkol
        //public async Task<ActionResult> Exportar(int page = 1, int recordsPerPage = 10)
        //{
        //    List<Modelos.Usuario> usuarios = new();
        //    int totalRegistros = 0;

        //    try
        //    {

        //        using var httpClient = new HttpClient();
        //        string apiUrl = "https://tu-api-url.com/api/usuarios";
        //        string token = "tu-token-aqui";
        //        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        //        // Si necesitas otros encabezados, los agregas de esta forma:
        //        httpClient.DefaultRequestHeaders.Add("Custom-Header", "HeaderValue");
        //        HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var jsonResponse = await response.Content.ReadAsStringAsync();
        //            Console.WriteLine(jsonResponse);

        //            // Configurar opciones para la deserialización
        //            var options = new JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true
        //            };

        //            var apiData = JsonSerializer.Deserialize<List<Modelos.Usuario>>(jsonResponse, options);

        //            if (apiData != null)
        //            {
        //                usuarios = apiData.Select(apiUsuario => new Modelos.Usuario
        //                {
        //                    IdBa = apiUsuario.IdBa,
        //                    Nombre = apiUsuario.Nombre,
        //                    ApellidoP = apiUsuario.ApellidoP,
        //                    ApellidoM = apiUsuario.ApellidoM
        //                }).ToList();

        //                totalRegistros = usuarios.Count;
        //            }
        //        }
        //        else
        //        {
        //            ViewBag.Error = "Error al consumir la API.";
        //            return View();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error al deserializar: {ex.Message}");
        //        ViewBag.Error = "Error al procesar los datos de la API.";
        //        return View();
        //    }

        //    if (recordsPerPage == 0)
        //    {
        //        recordsPerPage = totalRegistros;
        //    }

        //    var registrosPorPaginaOpciones = new List<int>();
        //    for (int i = 10; i < totalRegistros; i += 10)
        //    {
        //        registrosPorPaginaOpciones.Add(i);
        //    }
        //    registrosPorPaginaOpciones.Add(totalRegistros);

        //    var pagedUsuarios = usuarios
        //        .Skip((page - 1) * recordsPerPage)
        //        .Take(recordsPerPage)
        //        .ToList();

        //    var model = new Modelos.UsuarioViewModel
        //    {
        //        ListUsuarios = pagedUsuarios,
        //        CurrentPage = page,
        //        TotalPages = (int)Math.Ceiling(totalRegistros / (double)recordsPerPage),
        //        TotalRegistros = totalRegistros,
        //        RegistrosPorPagina = recordsPerPage,
        //        RegistrosPorPaginaOpciones = registrosPorPaginaOpciones
        //    };

        //    return View(model);
        //}


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


        public ActionResult Exportar(int page = 1, int recordsPerPage = 10)
        {
            Modelos.Usuario usuario = new Modelos.Usuario();
            Modelos.Result resultUs = Metodos.Usuario.GetAll(usuario);

            if (resultUs.Correct)
            {
                var usuarios = resultUs.Objects.Cast<Modelos.Usuario>().ToList();
                var totalRegistros = usuarios.Count;

                if (recordsPerPage == 0)
                {
                    recordsPerPage = totalRegistros; // Mostrar todos los registros si se selecciona "Todos"
                }

                var registrosPorPaginaOpciones = new List<int>();
                for (int i = 10; i < totalRegistros; i += 10)
                {
                    registrosPorPaginaOpciones.Add(i);
                }
                registrosPorPaginaOpciones.Add(totalRegistros); // Añadir la opción de "Todos"

                var pagedUsuarios = usuarios
                    .Skip((page - 1) * recordsPerPage)
                    .Take(recordsPerPage)
                    .ToList();

                var model = new Modelos.UsuarioViewModel
                {
                    ListUsuarios = pagedUsuarios,
                    CurrentPage = page,
                    TotalPages = (int)Math.Ceiling(totalRegistros / (double)recordsPerPage),
                    TotalRegistros = totalRegistros,
                    RegistrosPorPagina = recordsPerPage, // El valor que se seleccionó debe reflejarse aquí
                    RegistrosPorPaginaOpciones = registrosPorPaginaOpciones
                };

                return View(model);
            }
            else
            {
                ViewBag.Error = "Error en el proceso";
                return View();
            }
        }

        public IActionResult CrearNotificacionPush()
        {
            return View(); // Retorna la vista de crear notificación
        }

        [HttpPost]
        public IActionResult GuardarNotificacion(string tituloPush, string descripcionPush, string estatusPush,
                                          string tituloListado, string descripcionListado,
                                          IFormFile iconoPush, IFormFile iconoListado, IFormFile mediaListado)
        {
            // Procesar los datos aquí
            // Puedes guardar los archivos, validarlos y luego procesar los demás campos

            if (iconoPush != null)
            {
                // Guardar el archivo en alguna ruta, por ejemplo
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", iconoPush.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    iconoPush.CopyTo(stream);
                }
            }

            // Realiza operaciones con los demás archivos (iconoListado, mediaListado)
            // Luego responde a la petición
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // Guarda el archivo o realiza alguna acción aquí.
                // Puedes guardarlo en una carpeta del servidor, base de datos, etc.
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "C:\\Users\\PC FACTOR WHITE\\Documents\\Practicas\\Barber Alejandro\\CargasMasiva\\Pract_CargaMasv\\Pract_CargaMasv\\wwwroot\\Img\\", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Ok(new { status = "success", message = "File uploaded successfully" });
            }
            return BadRequest(new { status = "error", message = "File upload failed" });
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
