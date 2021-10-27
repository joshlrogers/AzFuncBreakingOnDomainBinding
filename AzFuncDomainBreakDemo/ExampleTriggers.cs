using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;

namespace CatchCo.DomainBreakDemo
{
    [StorageAccount("Azure_Storage_ConnectionString")]
    public static class ExampleTriggers
    {
        private const string CONTAINER_NAME = "test-container";
        private const string TABLE_NAME = "TestTableA";
        private const string QUEUE_NAME = "test-queue";

        [FunctionName("ExampleTimerTrigger")]
        public static void RunExampleTimerTrigger(
            [TimerTrigger("0 * */2 * * *")] TimerInfo myTimer,
            [Table(TABLE_NAME)] CloudTable table,
            ILogger log,
            ExecutionContext context)
        {
            log.LogInformation("[{InvocationId}] C# Timer trigger function triggered", context.InvocationId);
        }

        [FunctionName("ExampleHttpTrigger")]
        public static ActionResult RunExampleHttpTrigger(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = null)] HttpRequest req,
            [Table(TABLE_NAME)] CloudTable table,
            ILogger log,
            ExecutionContext context)
        {
            log.LogInformation("[{InvocationId}] C# HTTP trigger function triggered", context.InvocationId);
            return new OkObjectResult("You called, we answered...");
        }

        [FunctionName("ExampleBlobTrigger")]
        public static void RunBlobTrigger(
            [BlobTrigger(CONTAINER_NAME)] CloudBlockBlob blob,
            [Queue(QUEUE_NAME)] CloudQueue cloudQueue,
            ILogger log,
            ExecutionContext context)
        {
            log.LogInformation("[{InvocationId}] C# Blob trigger function triggered", context.InvocationId);
        }
        
        [FunctionName("ExampleQueueTrigger")]
        public static void RunBlobTrigger([QueueTrigger(QUEUE_NAME)] CloudQueue queue,
            ILogger log,
            ExecutionContext context)
        {
            log.LogInformation("[{InvocationId}] C# Queue trigger function triggered", context.InvocationId);
        }
    }
}