FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Lab8-JamilTurpo/Lab8-JamilTurpo.csproj", "Lab8-JamilTurpo/"]
RUN dotnet restore "Lab8-JamilTurpo/Lab8-JamilTurpo.csproj"
COPY . .
WORKDIR "/src/Lab8-JamilTurpo"
RUN dotnet build "./Lab8-JamilTurpo.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Lab8-JamilTurpo.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab8-JamilTurpo.dll"]
