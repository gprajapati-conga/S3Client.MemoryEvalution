using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace AWS.Nuget.MemoryValidation.Controllers
{
    [ApiController]
    [Route("api/aws/v1/[controller]")]
    public class S3NugetController : ControllerBase
    {

        private readonly ILogger<S3NugetController> _logger;
        private IAmazonS3 amazonS3Client;

        public S3NugetController(ILogger<S3NugetController> logger)
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
        public async Task<IActionResult> PutObjectAsync(int numberOfIterations, int batchSize)
        {
            Stream fileStream = new FileStream(Constants.InputFileName, FileMode.Open);
            byte[] byteArray = new byte[fileStream.Length];
            _ = fileStream.Read(byteArray, 0, byteArray.Length);
            int numberOfFilesUploaded = 0;
            for (int i = 0; i < numberOfIterations; i++)
            {
                _logger.LogInformation($"Started execution of iteration-{i}");
                var tasks = new List<Task>();
                for (int j = 0; j < batchSize; j++)
                {
                    tasks.Add(amazonS3Client.PutObjectAsync(new Amazon.S3.Model.PutObjectRequest()
                    {
                        InputStream = JsonSerializer.Deserialize<MemoryStream>(byteArray),
                        Key = $"{Constants.BasePath}/{Guid.NewGuid()}",
                        BucketName = Constants.BucketName
                    }));
                }
                _logger.LogInformation($"Added task to upload file for iteration-{i} and batch size = {tasks.Count}");
                await Task.WhenAll(tasks);
                numberOfFilesUploaded += tasks.Count;
                _logger.LogInformation($"Completed uploading {numberOfFilesUploaded} files");
            }

            return Ok($"Completed creating {numberOfFilesUploaded} files.");
        }
    }
}