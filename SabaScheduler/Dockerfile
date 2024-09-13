FROM hub.lumigame.com/base/dotnet/core/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src
COPY ["nuget.config", "."]
COPY ["SabaScheduler/SabaScheduler.csproj", "SabaScheduler/"]
COPY ["SabaScheduler.Core/SabaScheduler.Core.csproj", "SabaScheduler.Core/"]
RUN dotnet restore "./SabaScheduler/./SabaScheduler.csproj"

COPY . .
WORKDIR "/src/SabaScheduler"
RUN dotnet build "./SabaScheduler.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SabaScheduler.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM hub.lumigame.com/base/dotnet/core/aspnet:8.0-alpine-24h1.1 AS base
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SabaScheduler.dll"]