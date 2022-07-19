# Product Validation API

`Product Validation API` is an API that is created **solely for experimenting sync + async validation scenarios** by using `FluentValidation.NET` library.

Every validation scenario is tested on a dummy entity called `Product`. The validations are triggered when client calls `AddProduct` endpoint. 

The types of validations include:
- Sync validations
- Async validations
- Cross-field validations (field validation depending on another field)

The main purpose is to delegate every possible validation logic to custom implemented validators instead of using attributes on models. By achieving this, it becomes possible to *loosely couple model definition and its validations*.

## Table of Contents

- [Technologies](#technologies)
    - [FluentValidation](#fluent-validation)
    - [xUnit](#xunit)
    - [Moq](#moq)
    - [AutoFixture](#auto-fixture)
- [Prerequisites](#prerequisites)
- [How to build & run](#how-to-build-run)
- [Things to improve](#things-to-improve)


## <a name="technologies"></a> Technologies

The list below contains used technologies in this project:

- .NET 6
- FluentValidation
- xUnit
- Moq
- AutoFixture

### <a name="fluent-validation"></a> FluentValidation

FluentValidation provides a convenient API to write custom validators for entities. It reinforces separation of concerns by providing tools to handle validations separately from entity definitions. It also provides tools to inject and manage validators, which allows for flexible and maintainable validation handling across the board.

For more information, check out [docs](https://docs.fluentvalidation.net/en/latest/).

For more detailed information of why the validation is implemented the way it is in this project, please refer to corresponding ADR file in the repo.

<hr>

### <a name="x-unit"></a> xUnit

xUnit is one of the most commonly used unit testing tool for .NET ecosystem.

For more information, check out [docs](https://xunit.net/#documentation).

<hr>

### <a name="moq"></a> Moq

Moq is a helper library that streamlines creating and managing mocks for tests. Coupled with its low barrier of entry, it is a really powerful tool that greatly enhances developer experience in testing.

For more information, check out [their official Github repo](https://github.com/moq/moq4).

<hr>

### <a name="auto-fixture"></a> AutoFixture

AutoFixture is a helper library to make test setups easier.

It is possible to create a powerful test setup base when combined with Moq. This is how this library is actually used in this project.

For more information, check out [docs](https://autofixture.github.io/docs/quick-start/#).

For more detailed information about the usage in this project, please refer to corresponding ADR file in the repo.

<hr>

## <a name="prerequisites"></a> Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://docs.docker.com/get-docker/)

If you want to debug & play around in your own machine instead of messing around with Docker, you need to install a database. This project uses SQL Server, thus you need to install [SQL Server Dev or Express edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) as well.

<hr>

## <a name="how-to-build-run"></a> How to build & run

If you are using an IDE and want to debug on your machine, you can do so from your IDE.

If you want to access the application with Docker, you need to go into the project root path on your machine and build and boot up the containers by using the command below:

```bash
cd path/to/the/project/root && docker compose up -d
```

This will start up necessary service containers below:

- `product-validation-api`
- `product-validation-db`

No further configuration is needed, thus the containers should run without any issues. To check this, you can use the command below to see attached containers' status:

```bash
docker ps -a
```

At this point, you should be able to visit the Swagger endpoint to see a detailed information about all the endpoints.

Type the URL below to your browser to start playing with app:

```bash
http://localhost:8000/swagger/index.html
```

<hr>

## <a name="things-to-improve"></a> Things to improve

Since the main purpose was to mess around and get comfortable with FluentValidation, some things are done without best practices in mind. These issues will be handled later either in this project or in other repositories.

A little disclaimer: 

At this point, I value small projects and learning one step at a time, so I prefer many different small projects that focuses on specific parts of *backend* instead of one big project that tries to handle everything perfectly. That is why the points below can be considered as *technical debts*.

### Debugging on Docker containers

Currently, API container only runs in development environment and debugging the container is not supported.

Debugging the app is only possible with IDE at the moment. User needs to install SQL Server in his/her machine, and then needs to change the `ConnectionString` in `appsettings.json` to be able to build and debug the app.

### Logging

Logging is not configured in this project. I need to create a focused project to grasp the general idea, play around with it and then implement to other projects. So I skipped in this project to not spend additional time.

### Field based validation

Currently, custom validator is only used for validating the entire entity. However, at some cases it is required to support single field validation as well.

Clients should be able to request partial validation of an entity to notify users whether the input they entered is valid or not.

So implementing this feature is actually helpful in terms of real world practice but it is left out due to time constraints.

### Converting into a pure validation service

A microservice architecture can be implemented to separate business logics even further.
Instead of defining `Product` in this service, a separate domain microservice can be written (e.g `ProductApi`) and this new service can communicate with validation service through queues (e.g RabbitMQ). 

However, this means business related entity validations should be moved somewhere else as well. In this scenario, validation service should not contain any entity specific validation. But, it should be able to do entity specific validation as well as common validations.

This feature is really a contender to become v2 of this repository.
