#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CompanyStoreService/CompanyStoreService/CompanyStoreService.csproj", "CompanyStoreService/CompanyStoreService/"]
COPY ["Common/Contracts/Contracts.csproj", "Common/Contracts/"]
COPY ["Common/Dependencies/Dependencies.csproj", "Common/Dependencies/"]
COPY ["CompanyStoreService/Application/Application.csproj", "CompanyStoreService/Application/"]
COPY ["CompanyStoreService/Domain/Domain.csproj", "CompanyStoreService/Domain/"]
COPY ["CompanyStoreService/Infrastructure/Infrastructure.csproj", "CompanyStoreService/Infrastructure/"]
RUN dotnet restore "./CompanyStoreService/CompanyStoreService/CompanyStoreService.csproj"
COPY . .
WORKDIR "/src/CompanyStoreService/CompanyStoreService"
RUN dotnet build "./CompanyStoreService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CompanyStoreService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CompanyStoreService.dll"]