using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerFileEasyAzure.DTO
{
    /// <summary>
    /// Identifica el archivo generado
    /// </summary>
    public class GeneratedFileDto
    {
        /// <summary>
        /// nombre del archivo
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Archivo Generado
        /// </summary>
        public List<byte> File { get; set; }
        /// <summary>
        /// MimeType del archivo retornado
        /// </summary>
        public string MimeType { get; set; }
    }
}
