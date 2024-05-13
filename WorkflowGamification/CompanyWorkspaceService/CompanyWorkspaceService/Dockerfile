#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CompanyWorkspaceService/CompanyWorkspaceService/CompanyWorkspaceService.csproj", "CompanyWorkspaceService/CompanyWorkspaceService/"]
COPY ["Common/Contracts/Contracts.csproj", "Common/Contracts/"]
COPY ["Common/Dependencies/Dependencies.csproj", "Common/Dependencies/"]
COPY ["CompanyWorkspaceService/Application/Application.csproj", "CompanyWorkspaceService/Application/"]
COPY ["CompanyWorkspaceService/Domain/Domain.csproj", "CompanyWorkspaceService/Domain/"]
COPY ["CompanyWorkspaceService/Infrastructure/Infrastructure.csproj", "CompanyWorkspaceService/Infrastructure/"]
RUN dotnet restore "./CompanyWorkspaceService/CompanyWorkspaceService/CompanyWorkspaceService.csproj"
COPY . .
WORKDIR "/src/CompanyWorkspaceService/CompanyWorkspaceService"
RUN dotnet build "./CompanyWorkspaceService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CompanyWorkspaceService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CompanyWorkspaceService.dll"]