using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackerconfig.Utilities
{
    class BlobUtils
    {
        public static CloudBlobContainer GetBlobContainer(string containerName)
        {

            var connection = ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString;

            // Parse the connection string and return a reference to the storage account.
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            //    CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connection);

            //Create the Blob service client
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //Create a container
            //Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();

            return container;
        }

        public static void UploadRaceBlob(string containerName, IEnumerable<FileStream> courseFiles)
        {

            CloudBlobContainer container = GetBlobContainer(containerName);

            // Retrieve reference to the blob
            CloudBlockBlob tomrBlob = null;

            foreach (var file in courseFiles)
            {
                tomrBlob = container.GetBlockBlobReference(Path.GetFileNameWithoutExtension(file.Name));
                tomrBlob.UploadFromStream(file);
            }
        }

        public static void UploadRaceBlob(string containerName, Stream outputJsonLocation, string raceId)
        {

            CloudBlobContainer container = GetBlobContainer(containerName);

            // Retrieve reference to the blob
            CloudBlockBlob tomrBlob = container.GetBlockBlobReference(raceId);
            tomrBlob.UploadFromStream(outputJsonLocation);
            Console.WriteLine($"Uploading to blob storage successful: {raceId}");
        }

        public static void DownloadRaceBlob(CloudBlobContainer container, IEnumerable<FileStream> courseFiles)
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
