using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpxToJson
{
    class BlobUtils
    {
        public CloudBlobContainer GetBlobContainer(string containerName)
        {
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            //Create the Blob service client
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //Create a container
            //Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();

            return container;
        }

        public void UploadRaceBlob(CloudBlobContainer container, IEnumerable<FileStream> courseFiles)
        {
            // Retrieve reference to the blob
            CloudBlockBlob tomrBlob = null;
            foreach (var file in courseFiles)
            {
                tomrBlob = container.GetBlockBlobReference(Path.GetFileNameWithoutExtension(file.Name));
                tomrBlob.UploadFromStream(file);
            }
        }

        public void DownloadRaceBlob(CloudBlobContainer container, IEnumerable<FileStream> courseFiles)
        {
            // Retrieve reference to the blob
            CloudBlockBlob tomrBlob = null;
            foreach (var file in courseFiles)
            {
                tomrBlob = container.GetBlockBlobReference(Path.GetFileNameWithoutExtension(file.Name));
                tomrBlob.DownloadToStream(file);
            }
        }
    }
}
