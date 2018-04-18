using AzureFunctionApplication.Common.Domain;
using AzureFunctionApplication.Common.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Twilio;

namespace AzureFunctionApplication.Functions
{
    public static class LeaveRequestReceived
    {
        [FunctionName("LeaveRequestReceived")]
        [return: TwilioSms(AccountSidSetting = "TwilioAccountSid", AuthTokenSetting = "TwilioAuthToken", From = "+19473338191")]
        public static SMSMessage Run([QueueTrigger("leave-requests", Connection = "AzureWebJobsStorage")]LeaveRequestModel model,
            [DocumentDB("azure-functions-demo", "employees", Id = "{employeeId}", ConnectionStringSetting = "CosmosDbConnectionString")] Employee employee,
            [DocumentDB("azure-functions-demo", "leave-requests", ConnectionStringSetting = "CosmosDbConnectionString")] out LeaveRequest leaveRequest,
            TraceWriter log)
        {
            leaveRequest = new LeaveRequest(employee, model.NoOfDays);
            
            var message = new SMSMessage();
            message.Body = $"Hello {employee.FirstName}, we received your leave request for {model.NoOfDays} day/s";
            message.To = employee.MobileNumber;

            return message;
        }
    }
}
