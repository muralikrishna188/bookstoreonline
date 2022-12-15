using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Text.Json;

using System.Collections.Generic;

namespace AzureCourse.Function
{
    public static class CosmosOrderFunction
    {
        [FunctionName("ProcessOrderCosmos")]
        public static void Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            //[CosmosDB(databaseName: "readit-orders", collectionName: "orders", ConnectionStringSetting = "CosmosDBConnection")]out Order order,
            ILogger log)
        {
            string requestBody = new StreamReader(req.Body).ReadToEndAsync().Result;

            log.LogInformation($"Order JSON: {requestBody}");

            //order = JsonSerializer.Deserialize<Order>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}

