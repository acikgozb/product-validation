# This Dockerfile is taken from MSFT docs and adjusted as needed
# https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/building-net-docker-images?view=aspnetcore-6.0#the-dockerfile

# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# copy csproj and restore as distinct layers
COPY ./ProductValidation.sln .
COPY ./src/ProductValidation.API/ProductValidation.API.csproj /src/ProductValidation.API/
COPY ./src/ProductValidation.Core/ProductValidation.Core.csproj /src/ProductValidation.Core/
RUN dotnet restore "/src/ProductValidation.API/ProductValidation.API.csproj"

# copy everything else and build app
COPY ./src/ProductValidation.API/ /src/ProductValidation.API/
COPY ./src/ProductValidation.Core/ /src/ProductValidation.Core/
WORKDIR /src/ProductValidation.API
RUN dotnet publish -c Release -o ./publish --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /src/ProductValidation.API/publish ./
ENTRYPOINT ["dotnet", "ProductValidation.API.dll"]