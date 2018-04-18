using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace AzureFunctionApplication.Functions
{
    public static class LeaveRequestAdded
    {
        [FunctionName("LeaveRequestAdded")]
        public static void Run([CosmosDBTrigger(
              databaseName: "azure-functions-demo",
              collectionName: "leave-requests",
              ConnectionStringSetting = "CosmosDbConnectionString",
              CreateLeaseCollectionIfNotExists = true,
              LeaseCollectionName = "leave-requests-leases")]IReadOnlyList<Document> documents,
            TraceWriter log)
        {
            if (documents != null && documents.Count() > 0)
            {
                log.Info("Documents modified " + documents.Count());
            }
        }
    }
}
