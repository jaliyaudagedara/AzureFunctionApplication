using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AzureFunctionApplication.Common.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace AzureFunctionApplication.Functions
{
    public static class LeaveRequestSubmitted
    {
        [FunctionName("LeaveRequestSubmitted")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req,
            [Queue("leave-requests", Connection = "AzureWebJobsStorage")] IAsyncCollector<LeaveRequestModel> outputQueueItem,
            TraceWriter log)
        {
            LeaveRequestModel leaveRequest = await req.Content.ReadAsAsync<LeaveRequestModel>();

            await outputQueueItem.AddAsync(leaveRequest);

            return req.CreateResponse(HttpStatusCode.OK, "Leave Request Received");
        }
    }
}
