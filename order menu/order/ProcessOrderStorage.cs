using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.Azure.Storage.Blob;

namespace AzureCourse.Function
{
    public static class EventGridFunction
    {
        [FunctionName("ProcessOrderStorage")]
        public static void Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            [Blob("orders", FileAccess.Write, Connection = "StorageConnectionString")] CloudBlobContainer container,
            ILogger log)
        {
            string requestBody = new StreamReader(req.Body).ReadToEndAsync().Result;

            var blobName=Guid.NewGuid().ToString()+".txt";

            log.LogInformation($"blobName: {blobName}");
            log.LogInformation($"Order received: {requestBody}");

            var blockBlob=container.GetBlockBlobReference(blobName);
            blockBlob.UploadText(requestBody);

        }
    }
}

    