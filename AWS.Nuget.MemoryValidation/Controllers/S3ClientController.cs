using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace AWS.Nuget.MemoryValidation.Controllers
{
    [ApiController]
    [Route("api/aws/v1/[controller]")]
    public class S3ClientController : ControllerBase
    {

        private readonly ILogger<S3ClientController> _logger;
        private IAmazonS3 amazonS3Client;

        public S3ClientController(ILogger<S3ClientController> logger)
        {
            var amazonS3Config = new AmazonS3Config()
            {
                MaxErrorRetry = Constants.MaxRetryAttempts,
                ServiceURL = Constants.ServiceURL
            };
            _logger = logger;
            amazonS3Client = new AmazonS3Client(Constants.AccessKey, Constants.SecretKey, amazonS3Config);
        }

        [HttpGet("putobject")]
        public async Task<IActionResult> PutObjectAsync(int numberOfRequests)
        {
            _logger.LogInformation($"putobject request started: {numberOfRequests}");

            Stream fileStream = new FileStream(Constants.InputFileName, FileMode.Open);
            byte[] byteArray = new byte[fileStream.Length];
            _ = fileStream.Read(byteArray, 0, byteArray.Length);

            var tasks = new List<Task>();
            for (int j = 0; j < numberOfRequests; j++)
            {
                tasks.Add(amazonS3Client.PutObjectAsync(new Amazon.S3.Model.PutObjectRequest()
                {
                    InputStream = JsonSerializer.Deserialize<MemoryStream>(byteArray),
                    Key = $"{Constants.BasePath}/{Guid.NewGuid()}",
                    BucketName = Constants.BucketName
                }));
            }
            await Task.WhenAll(tasks);

            _logger.LogInformation($"putobject request completed.");

            return Ok($"Completed S3Client.PutObjectAsync requests: {numberOfRequests}");
        }
    }
}