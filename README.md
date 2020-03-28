# PANDEMIC LOCATOR

## This project aims to disseminate info about COVID-19 trough:
* Create a way for the population around the world tell about it's self healthness
* Sharing this project for public usage, evolving and gain time to go online ASAP to help in the COVID-19 battle
* An interface (first site, then mobile if have time and staff to help in building) to show illness people near you
  to help in adopt the cautions to stay at home.

## This application is intended for study and practice areas like
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
