# AWS.S3Nuget.MemoryValidation
Following are steps to run the application. Please note that docker is prerequsite to run this application. 
1. Clone the repository to local drive.
2. Run the application using "docker-compose up --build" command.
3. Open the application swagger page at - http://localhost:5090/api/aws/swagger/index.html
4. Run S3Client controller 'pubobject' with 2000 number of requests.

PUT OBJECT payload detail: Our single payload size is 500 bytes, for 2000 request = 500 * 2000 bytes = 1MB

To upload 1MB of data , when we run application it takes more than 100MB memory and container exiting with OOM.

