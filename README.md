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
* Updating my dotNet expertise to .NET Core 3 and C#8
* Updating my cloud experience in Azure to start using AWS
* Practicing queues using RabbitMQ
* Practicing cache using Redis
* Practicing geographic calculations using PostGIS and PostgreSQL
* Practicing heavy load techniques in code (async, cache, queues, circuit breaker) and hosting (LB, routes, gateways)
* Practing CI/CD using GitLab for DevOps

## Current Doubts
* Need a review in lifecycle of objects injected from IoC
* Best way to use PostGIS for calculations in illness map.
* How to accept anonymous health report and at the same time avoid accepting too many from the same reporter (person)
* Hot to use dotNet Core's health report to link to the load balancer and don't need to implement HealthReport/HealthCheck

## To setup your backend environment you will need:
* A local Docker for Postgres, Redis and Rabbit
* Create your appsettings to start using the API

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

  "QueueConnection": {
    "HostName": "<YOUR_IP>",
    "Port": 5672,
    "UserName": "<YOUR_INFO>",
    "Password": "<YOUR_INFO>"
  },

  "ConnectionStrings": {
    "PandemicDatabase": "Server=<YOUR_IP>;Port=5432;Database=pandemic;Userid=<USER>;Password=<PWD>;Pooling=true;MinPoolSize=1;MaxPoolSize=1024;",
    "RedisConnection": "<YOUR_IP>:6379"
  }
}
```
