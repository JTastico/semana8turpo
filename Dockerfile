# Base image (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Build stage (SDK)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["Lab8-JamilTurpo/Lab8-JamilTurpo.csproj", "Lab8-JamilTurpo/"]
RUN dotnet restore "Lab8-JamilTurpo/Lab8-JamilTurpo.csproj"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/Lab8-JamilTurpo"

# Build
RUN dotnet build "./Lab8-JamilTurpo.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Lab8-JamilTurpo.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Lab8-JamilTurpo.dll"]
