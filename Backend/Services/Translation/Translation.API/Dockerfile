# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Translation/Translation.API/Translation.API.csproj", "Services/Translation/Translation.API/"]
COPY ["Utils/General/General.csproj", "Utils/General/"]
COPY ["Services/Translation/Domain/Translation.Domain.csproj", "Services/Translation/Domain/"]
COPY ["Utils/BaseCrud/BaseCrud.Abstractions/BaseCrud.Abstractions.csproj", "Utils/BaseCrud/BaseCrud.Abstractions/"]
COPY ["Utils/BaseCrud/BaseCrud/BaseCrud.csproj", "Utils/BaseCrud/BaseCrud/"]
COPY ["Utils/Web.API/Web.API.csproj", "Utils/Web.API/"]
COPY ["Utils/BaseCrud/BaseCrud.PrimeNg/BaseCrud.PrimeNg.csproj", "Utils/BaseCrud/BaseCrud.PrimeNg/"]
COPY ["Utils/Clients/Clients.csproj", "Utils/Clients/"]
COPY ["Services/Translation/Application/Translation.Application.csproj", "Services/Translation/Application/"]
COPY ["Utils/BaseCrud/BaseCrud.EntityFrameworkCore/BaseCrud.EntityFrameworkCore.csproj", "Utils/BaseCrud/BaseCrud.EntityFrameworkCore/"]
COPY ["Services/Translation/GTranslate/GTranslate.csproj", "Services/Translation/GTranslate/"]
COPY ["Services/Translation/Infrastructure/Translation.Infrastructure.csproj", "Services/Translation/Infrastructure/"]
RUN dotnet restore "./Services/Translation/Translation.API/Translation.API.csproj"
COPY . .
WORKDIR "/src/Services/Translation/Translation.API"
RUN dotnet build "./Translation.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Translation.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Translation.API.dll"]