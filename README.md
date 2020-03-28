# PANDEMIC LOCATOR

## This is an open source project thant aims disseminating info about cases of coronavirus COVID-19 trough:
* Create a way for the population around the world tell about it's self healthness
* Sharing this project for public usage, evolving and gain time to go online ASAP to help in the COVID-19 battle
* An Website to show illness people near you
  to help in adopt the cautions to stay at home. Soon will be available https://www.pandemiclocator.io.

## Pandemic Locator is also a collaborative platform
### The idea is to provide a platform (initially Web) for the person to have knowledge of the cases of COVID-19 that are close to him (radius of 30km). The virus cases will be shown as points of interest (POI) on a map. The person can also contribute with case reports regarding the virus. The idea is to be a collaborative platform for the disclosure of cases in a totally anonymous way.

## This application is intended for study and practice areas like
* Open sourcing as activity and team work
* Updating my dotNet expertise to .NET Core 3 and C#7
* Updating my cloud experience in Azure to start using AWS
* Practicing queues using RabbitMQ
* Practicing cache using Redis
* Practicing NoSQL DB using DynamoDB
* Practicing heavy load techniques in code (async, cache, queues, circuit breaker) and hosting (LB, routes, gateways)
* Practicing geografic routines/algorithms necessary for the illness map
* Practing CI/CD using GitLab for DevOps

## How to setup your environment you will need:
* An AWS account for creating dynamoDB (I don't know a way to setup locally)
* A local Docker for Redis and Rabbit
* Create your appsettings to start using the API (what is in progress right now)

## Doubts
* Need a review in lifecycle of objects injected from IoC
* Best way to use DynamoDB in storing geographic lcoation for better calculations n  illness map.
* Using Razor for Frontend until have some more expertise staff in frontend
* How to accept anonymous health report and at the same time avoid accepting too many from the same reporter (person)
* Hot to use dotNet Core's health report to link to the load balancer and don't need to implement HealthReport/HealthCheck

## Example of appsettings.json
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",

  "DynamoDbConnection": {
    "AccessKey": "<YOUR_INFO>",
    "SecretKey": "<YOUR_INFO>",
    "System": "<YOUR_INFO> example: us-east-1"
  },

  "QueueConnection": {
    "HostName": "<YOUR_IP>",
    "Port": 5672,
    "UserName": "<YOUR_INFO>",
    "Password": "<YOUR_INFO>"
  },

  "ConnectionStrings": {
    "RedisConnection": "<YOUR_IP>:6379"
  }
}
```
