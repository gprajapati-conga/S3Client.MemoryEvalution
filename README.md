# AWS.S3Nuget.MemoryValidation
Following are steps to run the application. Please note that docker is prerequsite to run this application. 
1. Clone the repository to local drive.
2. In Constant.cs file, Update BucketName with proper value and make sure that named bucket is created and update ServiceURL based on the bucket location. Also, assign proper values for AccessKey and SecretKey using which we can access this bucket to create some records.
3. Now go to repository root folder and Run the application using "docker-compose up --build" command.
4. Open the application swagger page in some browser - http://localhost:5090/api/aws/swagger/index.html
5. Run S3Client controller GET - 'pubobject' API with 2000 number of parallel requests. This API runs 2000 parallel requests to s3client to create 2000 objects.

## Total size of objects

As our single object size is 500 bytes, Total size of 2000 objects = 500 * 2000 bytes = 1MB

## Observation: 

To upload 1MB of data , when we run application it takes more than 100MB memory, and our container with 100M memory limit exiting with 137 OOM.

## Questions:

1. Why more than 100MB application memory required to upload data having size of 1MB?
2. How to reduce pressure on the memory without losing the performance that we get by parallel calls?

