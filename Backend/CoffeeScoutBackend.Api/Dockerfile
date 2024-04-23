FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
RUN apt-get update && apt-get install -y curl

ARG TARGETARCH

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CoffeeScoutBackend.Api/CoffeeScoutBackend.Api.csproj", "CoffeeScoutBackend.Api/"]
RUN dotnet restore "CoffeeScoutBackend.Api/CoffeeScoutBackend.Api.csproj"
COPY . .
WORKDIR "/src/CoffeeScoutBackend.Api"
RUN dotnet build "CoffeeScoutBackend.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CoffeeScoutBackend.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoffeeScoutBackend.Api.dll"]
