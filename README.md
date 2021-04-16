# Razor MVC application

The infrastructure inspiration got [here](https://dev.to/alrobilliard/deploying-net-core-to-heroku-1lfe) 

Application is deployed to [heroku](https://borys-internship-class.herokuapp.com/)

## Prerequisites

To work with this application you need to install:
* [.Net Core SDK 5.0+](https://dotnet.microsoft.com/download/dotnet/5.0) - to run and develop the application
* [heroku CLI](https://devcenter.heroku.com/articles/heroku-cli) - to deploy the application
* [Postgres](https://www.postgresql.org/download/) - database engine (replaced MS SQL since 15.04)

## Build and run locally

```
dotnet build
```

to execute tests

```
dotnet test
```

to run
```
dotnet run
```

## Build and run in docker

```
docker build -t mvc_borys .
docker run -d -p 8081:80 --name mvc_container_borys mvc_borys
```

to stop container
```
docker stop mvc_container_borys
```

to remove container
```
docker rm mvc_container_borys
```

## CI

CI is implemented in [github](.github/workflows/dotnet.yml). The pipeline actions can be seen [here](https://github.com/borys-pc33/RazorMvc/actions)

## Deploy to heroku

1. Create heroku account
2. Create application
3. Choose container registry as deployment method
4. Make sure application works locally


Login to heroku
```
heroku login
heroku container:login
```

Push container
```
heroku container:push -a borys-internship-class web
```

Release the container
```
heroku container:release -a borys-internship-class web
```

## Using database

The default database is MSSQL.

The connection string to the database is specified in appsettings.json

```
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=Internship;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
```

To create database from scratch:
1. Create database with the name Internship (or whatever it is named in appsettings.json)
2. Apply migration
```
dotnet ef database update
```

To create migration

```
dotnet ef migrations add <migrationName>
```