﻿using System;
using System.IO;
using System.Threading.Tasks;
using Funkmap.Common.Abstract;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Funkmap.Azure
{
    public class AzureFileStorage : IFileStorage
    {
        private readonly CloudBlobContainer _container;

        public AzureFileStorage(CloudBlobClient blobClient, string containerName)
        {
            _container = blobClient.GetContainerReference(containerName);
            _container.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Container, new BlobRequestOptions(), new OperationContext());

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">Название файла С РАСШИРЕНИЕМ</param>
        /// <param name="bytes"></param>
        /// <returns></returns>

        public async Task<string> UploadFromBytesAsync(string fileName, byte[] bytes)
        {
            var blob = _container.GetBlockBlobReference($"{fileName}");
            await blob.UploadFromByteArrayAsync(bytes, 0, bytes.Length);
            return $"{_container.Uri}/{fileName}";
        }

        public async Task<byte[]> DownloadAsBytesAsync(string fullFilePath)
        {
            if (String.IsNullOrEmpty(fullFilePath)) throw new ArgumentException("Пустой путь файла");

            var name = fullFilePath.Replace($"{_container.Uri}/", "");
            CloudBlockBlob blob = _container.GetBlockBlobReference(name);
            if (blob == null) return null;

            byte[] result;

            using (var memoryStream = new MemoryStream())
            {
                await blob.DownloadToStreamAsync(memoryStream);
                result = memoryStream.ToArray();
            }
            return result;
        }

        public async Task DeleteAsync(string fullFilePath)
        {
            var name = fullFilePath.Replace($"{_container.Uri}/", "");
            CloudBlockBlob blob = _container.GetBlockBlobReference(name);
            await blob.DeleteAsync();
        }
    }
}
