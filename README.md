- Challenge RESTful API
- Target Framework .Net 8.0 with .NET Aspire
- Build and debug Visual Studio 2022 with .Net8 SDK

- Database instantiation is done via CustomerContext using SQLite and stored in an db file (test.db) with the application directory
- An overload would be useful to change the file location or type depending on the mode of operation and I would add that in the future.

- Unit tests of repository methods [though not finished] are within the test project BL.Challenge.Test
- Integration Test of API controller methods [Again, could use expanding] are with test project BL.Challenge.Test
- While currently checked into GIT repository, logging methods are extended for use with the Azure DevOps pipelines for implementation during CI/CD.
- Pipeline Logger is formated to work with Azure DevOps VsTest and log failed tests. The test step can be configured to fail a build if a test class fails.

- The CustomerRepository utilizes constructor based DI to handle different logging depending on whether the code is being run from a development/test/production environment providing greater visibility.

- A docker container has been created and is running under an AWS ECS cluster and delivered via Elastic Load Balancer: http://BLChal-Recip-3gvOjdDm1Ecs-841706532.us-east-1.elb.amazonaws.com

 The methods can be accessed via CURL with the following definitions 
 curl -X 'GET' \
  'http://BLChal-Recip-3gvOjdDm1Ecs-841706532.us-east-1.elb.amazonaws.com/api/Customer' \
  -H 'accept: text/plain'

  curl -X 'POST' \
  'http://BLChal-Recip-3gvOjdDm1Ecs-841706532.us-east-1.elb.amazonaws.com/api/Customer' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": 0,
  "firstName": "string",
  "middleName": "string",
  "lastName": "string",
  "emailAddress": "user@example.com",
  "phone": "string"
}'

curl -X 'GET' \
  'https://localhost:7283/api/Customer/1' \
  -H 'accept: text/plain'

curl -X 'PUT' \
  'https://localhost:7283/api/Customer/1' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": 1,
  "firstName": "string",
  "middleName": "string",
  "lastName": "string",
  "emailAddress": "user@example.com",
  "phone": "string"
}'

curl -X 'DELETE' \
  'https://localhost:7283/api/Customer/1' \
  -H 'accept: */*'
