using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using ClotheStore.Application.ViewModels;
using ClotheStore.Domain.Core;
using ClotheStore.Domain.Repositories;
using Microsoft.Extensions.Options;



namespace ClotheStore.Application.Queries
{
    public class BlobQueryService(BlobServiceClient blobServiceClient,
        IBlobRepository blobRepository,
        IOptions<AppSettings> options) : IBlobQueryService
    {
        public async Task<string> GetUploadUrl(BlobUrlRequestVM request)
        {
            try
            {
                string imageUrl = "";

                if (request.Image != null)
                {
                    var connectionString = options.Value.AzureStorage.ConnectionString;
                    var extension = Path.GetExtension(request.Image.FileName);
                    var fileName = $"{Guid.NewGuid()}{extension}";

                    var blobServiceClient = new BlobServiceClient(connectionString);
                    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("design-preview");
                    BlobClient blobClient = containerClient.GetBlobClient(fileName);
                    using (var stream = new MemoryStream())
                    {
                        await request.Image.CopyToAsync(stream);

                        stream.Position = 0;
                        var headers = new BlobHttpHeaders
                        {
                            ContentType = request.Image.ContentType
                        };

                        await blobClient.UploadAsync(stream, new BlobUploadOptions
                        {
                            HttpHeaders = headers
                        });

                        //var resul = await blobClient.UploadAsync(stream, overwrite: true);

                        var sasUri = blobClient.GenerateSasUri(
                            BlobSasPermissions.Read,
                            DateTimeOffset.UtcNow.AddMinutes(30));


                        imageUrl = sasUri.ToString();
                    }
                }
                return imageUrl;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return null;
            }
        }

        public async Task<Uri> GetUploadUrl2(BlobUrlRequestVM request)
        {
            // 1️⃣ Validate input
            var allowedTypes = new[] { "image/png", "image/jpeg", "image/webp" };
            //if (!allowedTypes.Contains(request.ContentType))
            //    return BadRequest("Invalid image type");

            //if (request.FileSize > 5 * 1024 * 1024)
            //    return BadRequest("File too large");

            //var blobName = $"{Guid.NewGuid()}{Path.GetExtension(request.FileName)}";
            //var result = await GenerateUploadSasAsync("design-temp", blobName);
            //return result;

            return null;

            //var container = blobServiceClient.GetBlobContainerClient("design-temp");
            //var blobClient = container.GetBlobClient(blobName);

            //var sas = blobClient.GenerateSasUri(
            //    BlobSasPermissions.Write | BlobSasPermissions.Create,
            //    DateTimeOffset.UtcNow.AddMinutes(10)
            //);

            //blobRepository.InsertImage(new Image
            //{
            //    BlobName = blobName,
            //    Container = "design-temp",
            //    UserId = request.UserId,
            //    Status = "Temp"
            //});

            //var result = new ImageUploadResponseVM
            //{
            //    UploadUrl = sas.ToString(),
            //    BlobName = blobName
            //};

            //return result;
        }

        private async Task<Uri> GenerateUploadSasAsync(string containerName, string blobName)
        {
            try
            {

                var container = blobServiceClient.GetBlobContainerClient(containerName);
                var blobClient = container.GetBlobClient(blobName);

                // 1️⃣ Get User Delegation Key
                var delegationKey = await blobServiceClient.GetUserDelegationKeyAsync(
                    DateTimeOffset.UtcNow,
                    DateTimeOffset.UtcNow.AddMinutes(15)
                );

                // 2️⃣ Create SAS builder
                var sasBuilder = new BlobSasBuilder
                {
                    BlobContainerName = containerName,
                    BlobName = blobName,
                    Resource = "b",
                    ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(10)
                };

                sasBuilder.SetPermissions(BlobSasPermissions.Write | BlobSasPermissions.Create);

                // 3️⃣ Generate SAS using delegation key
                //var sasUri = blobClient.GenerateSasUri(sasBuilder, delegationKey.Value);

                var sasToken = sasBuilder
                .ToSasQueryParameters(delegationKey.Value, blobServiceClient.AccountName)
                .ToString();

                var sasUri = new Uri($"{blobClient.Uri}?{sasToken}");

                return sasUri;
            }
            catch(Exception ex)
            {
                throw new Exception();
            }




            //var sasBuilder = new BlobSasBuilder
            //{
            //    BlobContainerName = containerName,
            //    BlobName = blobName,
            //    Resource = "b",
            //    ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(10)
            //};

            //sasBuilder.SetPermissions(BlobSasPermissions.Write | BlobSasPermissions.Create);

            //var sasUri = blobClient.GenerateSasUri(sasBuilder);

            //return sasUri;
        }
    }
}
