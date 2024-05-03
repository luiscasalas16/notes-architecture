#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NCA.Common/NCA.Common.Scheduler/NCA.Common.Scheduler.csproj", "NCA.Common/NCA.Common.Scheduler/"]
COPY ["NCA.Common/NCA.Common.Api/NCA.Common.Api.csproj", "NCA.Common/NCA.Common.Api/"]
COPY ["NCA.Common/NCA.Common.Application/NCA.Common.Application.csproj", "NCA.Common/NCA.Common.Application/"]
COPY ["NCA.Common/NCA.Common.Domain/NCA.Common.Domain.csproj", "NCA.Common/NCA.Common.Domain/"]
COPY ["NCA.Common/NCA.Common.Infrastructure.Log/NCA.Common.Infrastructure.Log.csproj", "NCA.Common/NCA.Common.Infrastructure.Log/"]
RUN dotnet restore "./NCA.Common/NCA.Common.Scheduler/NCA.Common.Scheduler.csproj"
COPY . .
WORKDIR "/src/NCA.Common/NCA.Common.Scheduler"
RUN dotnet build "./NCA.Common.Scheduler.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NCA.Common.Scheduler.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NCA.Common.Scheduler.dll"]