namespace AWS.Nuget.MemoryValidation
{
    public class Constants
    {
        public const string AccessKey = "****************";
        public const string SecretKey = "****************";
        public const string ServiceURL = "https://s3.<<awa-region>>.amazonaws.com";
        public const int MaxRetryAttempts = 3;
        public const string BucketName = "s3client-memorytest";
        public const string BasePath = "data";
        public const string InputFileName = @"Data/500bytes.json";
    }
}
