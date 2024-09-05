using System;
using System.Data.SqlClient;
using System.IO;
using ClosedXML.Excel;

namespace Metodos
{
    public class CargaMasiva
    {
        public string ExportToExcel(string connectionString, string query)
        {
            try
            {
                // Nombre base del archivo
                string baseFileName = $"CargaMasiv_{DateTime.Now:yyyyMMdd}";
                string fileName = GetUniqueFileName(baseFileName, "xlsx");

                // Configurar opciones de Excel con ClosedXML
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Hoja1");
                int rowCount = 0;
                int sheetIndex = 1;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Escribir encabezados
                            for (int col = 0; col < reader.FieldCount; col++)
                            {
                                worksheet.Cell(1, col + 1).Value = reader.GetName(col);
                            }
                            while (reader.Read())
                            {
                                // Crear una nueva hoja cada 1000 registros
                                if (rowCount % 1000 == 1000)
                                {
                                    sheetIndex++;
                                    worksheet = workbook.Worksheets.Add($"Hoja{sheetIndex}");
                                    for (int col = 0; col < reader.FieldCount; col++)
                                    {
                                        worksheet.Cell(1, col + 1).Value = reader.GetName(col);
                                    }
                                }

                                rowCount++;
                                for (int col = 0; col < reader.FieldCount; col++)
                                {
                                    object value = reader[col]; // Obtener el valor del campo

                                    // Asignar el valor a la celda de Excel adecuadamente
                                    // Convertir el valor a string según corresponda
                                    worksheet.Cell(rowCount % 1000 + 1, col + 1).Value = value.ToString();
                                }
                            }
                        }
                    }
                }

                // Guardar el archivo Excel con ClosedXML
                workbook.SaveAs(fileName);
                Console.WriteLine($"Archivo Excel creado: {fileName}");

                // Retornar la ruta del archivo creado
                return fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al exportar datos a Excel: {ex.Message}");
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
                return null;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
            }
        }

        // Método para obtener un nombre de archivo único en caso de colisión
        private string GetUniqueFileName(string baseFileName, string extension)
        {
            string fileName = $"{baseFileName}.{extension}";
            int fileCount = 1;
            string directory = Path.Combine(Environment.CurrentDirectory, "CargaMasivos");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            while (File.Exists(Path.Combine(directory, fileName)))
            {
                fileName = $"{baseFileName}_{fileCount}.{extension}";
                fileCount++;
            }

            return Path.Combine(directory, fileName);
        }
    }
}
