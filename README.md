# AWS.S3Nuget.MemoryValidation
Following are steps to run the application. Please note that docker is prerequsite to run this application. 
1. Clone the repository to local drive.
2. Update Constant.cs file, provide proper values for AccessKey and SecretKey who have access to upload data to s3client-memorytest bucket. Also, update ServiceURL based on the region of the bucket. Also update BucketName with proper value and make sure that named bucket is created and available.
3. Go to repository folder and Run the application using "docker-compose up --build" command.
4. Open the application swagger page in some browser - http://localhost:5090/api/aws/swagger/index.html
5. Run S3Client controller GET - 'pubobject' API with 2000 number of requests. Before running the application please make sure that we need to replace accesskey and secretkey in Constant.cs file.

PUT OBJECT payload detail: Our single payload size is 500 bytes, for 2000 request = 500 * 2000 bytes = 1MB

To upload 1MB of data , when we run application it takes more than 100MB memory and container exiting with OOM.

