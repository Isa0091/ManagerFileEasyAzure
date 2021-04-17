using ManagerFileEasyAzure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerFileEasyAzure
{
    /// <summary>
    /// Maneja los metodos de carga de archivos a azure
    /// </summary>
    public interface IManagerFileEasyAzureProvider
    {
        /// <summary>
        /// Elimina un archivo apartir de la url proporcionada
        /// </summary>
        /// <param name="FileUrl"></param>
        /// <returns></returns>
        Task<bool> DeleteFile(Uri FileUrl);

        /// <summary>
        /// agrega un archivo atrvez  de sus byte al storage de azure
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="fileName"></param>
        /// <param name="mimeType"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        Task<Uri> PostFileStorageAsync(byte[] contents, string fileName, string mimeType, bool overwrite = false);

        /// <summary>
        /// Genera los datos en bytes de un archivo atravez de su url obteniada al momento de enviarlo al storage
        /// </summary>
        /// <param name="urlFile"></param>
        /// <returns></returns>
        Task<GeneratedFileDto> DowloadFileByteAsync(Uri urlFile);

        /// <summary>
        /// Agrega al storage una imaen en base64 al storage
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="contentsBase64"></param>
        /// <param name="mimeType"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        Task<Uri> PostImageBase64InStorage(string fileName, string contentsBase64, string mimeType, bool overwrite = false);
    }
}
