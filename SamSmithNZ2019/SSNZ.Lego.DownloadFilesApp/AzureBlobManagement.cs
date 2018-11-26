using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SSNZ.Lego.DownloadFilesApp
{
    public static class AzureBlobManagement
    {
        //Load up to the storage account, adapted from the Azure quick start for blob storage: 
        //https://github.com/Azure-Samples/storage-blobs-dotnet-quickstart/blob/master/storage-blobs-dotnet-quickstart/Program.cs
        public static async Task UploadFilesToStorageAccountBlobs(string storageConnectionString, string containerName, string tempFolderLocation, List<string> files, bool filesHaveFullPath)
        {
            CloudStorageAccount storageAccount = null;
            CloudBlobContainer cloudBlobContainer = null;
            //string sourceFile = null;
            //string destinationFile = null;

            // Retrieve the connection string for use with the application. The storage connection string is stored
            // in an environment variable on the machine running the application called storageconnectionstring.
            // If the environment variable is created after the application is launched in a console or with Visual
            // Studio, the shell needs to be closed and reloaded to take the environment variable into account.
            //string storageConnectionString = Environment.GetEnvironmentVariable("storageconnectionstring");

            // Check whether the connection string can be parsed.
            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                try
                {
                    // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                    // Create a container called 'quickstartblobs' and append a GUID value to it to make the name unique. 
                    cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
                    if (await cloudBlobContainer.ExistsAsync() == false)
                    {
                        await cloudBlobContainer.CreateAsync();
                    }
                    Console.WriteLine("Created container '{0}'", cloudBlobContainer.Name);
                    Console.WriteLine();

                    // Set the permissions so the blobs are offs. 
                    BlobContainerPermissions permissions = new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Off
                    };
                    await cloudBlobContainer.SetPermissionsAsync(permissions);

                    foreach (string file in files)
                    {
                        string fileToUpload = file;
                        if (filesHaveFullPath == false)
                        {
                            fileToUpload = tempFolderLocation + @"\" + file;
                        }
                        Console.WriteLine("Uploading to Blob storage as blob '{0}'", file);
                        Console.WriteLine();

                        // Get a reference to the blob address, then upload the file to the blob.
                        // Use the value of localFileName for the blob name.
                        if (File.Exists(fileToUpload) == true)
                        {
                            //Now strip the full path off so we can store any folders with the files we extracted
                            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileToUpload.Replace(tempFolderLocation + @"\", ""));
                            await cloudBlockBlob.UploadFromFileAsync(fileToUpload);
                        }
                        else
                        {
                            Console.WriteLine("File '" + fileToUpload + "' not found...");
                        }
                    }

                    // List the blobs in the container.
                    Console.WriteLine("Listing blobs in container.");
                    BlobContinuationToken blobContinuationToken = null;
                    do
                    {
                        var results = await cloudBlobContainer.ListBlobsSegmentedAsync(null, blobContinuationToken);
                        // Get the value of the continuation token returned by the listing call.
                        blobContinuationToken = results.ContinuationToken;
                        foreach (IListBlobItem item in results.Results)
                        {
                            Console.WriteLine(item.Uri);
                        }
                    } while (blobContinuationToken != null); // Loop while the continuation token is not null.
                    Console.WriteLine();
                }
                catch (StorageException ex)
                {
                    Console.WriteLine("Error returned from the service: {0}", ex.Message);
                }
            }
            else
            {
                Console.WriteLine(
                    "A connection string has not been defined in the system environment variables. " +
                    "Add a environment variable named 'storageconnectionstring' with your storage " +
                    "connection string as a value.");
            }
        }
    }
}
