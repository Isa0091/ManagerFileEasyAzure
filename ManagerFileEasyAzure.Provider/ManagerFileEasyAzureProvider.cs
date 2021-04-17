using ManagerFileEasyAzure.DTO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerFileEasyAzure.Provider
{
    public class ManagerFileEasyAzureProvider : IManagerFileEasyAzureProvider
    {
        private string _connection;
        private string _container;

        public ManagerFileEasyAzureProvider(string connection, string container)
        {
            _connection = connection;
            _container = container;
        }

        public async Task<bool> DeleteFile(Uri FileUrl)
        {
            string fileName = System.IO.Path.GetFileName(FileUrl.ToString());
            CloudBlockBlob blob = GetBlob(_connection, _container, fileName);
            return await blob.DeleteIfExistsAsync();
        }
        public async Task<Uri> PostFileStorageAsync(byte[] contents, string fileName, string mimeType, bool overwrite = false)
        {
            string withoutExtension = System.IO.Path.GetFileNameWithoutExtension(fileName);
            string extension = System.IO.Path.GetExtension(fileName);

            CloudStorageAccount account = CloudStorageAccount.Parse(_connection);
            CloudBlobClient client = account.CreateCloudBlobClient();
            CloudBlobContainer container = client.GetContainerReference(_container);

            CloudBlockBlob blob = container.GetBlockBlobReference(fileName);

            int i = 1;
            while (overwrite == false && await blob.ExistsAsync())
            {
                blob = container.GetBlockBlobReference($"{withoutExtension}{i}{extension}");
                i = i + 1;
            }

            blob.Properties.ContentType = mimeType;
            await blob.UploadFromByteArrayAsync(contents, 0, contents.Length);

            return blob.Uri;
        }
        public async Task<GeneratedFileDto> DowloadFileByteAsync(Uri urlFile)
        {
            GeneratedFileDto generatedFileDto = new GeneratedFileDto();
            string fileName = System.IO.Path.GetFileName(urlFile.ToString());
            CloudBlockBlob blob = GetBlob(_connection, _container, fileName);

            if (await blob.ExistsAsync() == false)
                throw new FileNotFoundException();

            await blob.FetchAttributesAsync();
            long fileByteLength = blob.Properties.Length;

            byte[] file = new byte[fileByteLength];
            await blob.DownloadToByteArrayAsync(file, 0);

            generatedFileDto.File = file.ToList();
            generatedFileDto.FileName = fileName;
            generatedFileDto.MimeType = blob.Properties.ContentType;

            return generatedFileDto;
        }
        private CloudBlockBlob GetBlob(string connection, string containerName, string fileName)
        {
            CloudStorageAccount account = CloudStorageAccount.Parse(connection);
            CloudBlobClient client = account.CreateCloudBlobClient();

            CloudBlobContainer contenedor = client.GetContainerReference(containerName);

            return contenedor.GetBlockBlobReference(fileName);
        }
        public async Task<Uri> PostImageBase64InStorage(string fileName, string contentsBase64, string mimeType, bool overwrite = false)
        {
            byte[] contents = Convert.FromBase64String(contentsBase64);
            return await PostFileStorageAsync(contents, fileName, mimeType, overwrite);
        }


    }
}