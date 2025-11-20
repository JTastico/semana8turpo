# ====== BUILD STAGE ======
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos la solución y restauramos
COPY Lab8-JamilTurpo.sln ./
COPY Lab8-JamilTurpo/*.csproj Lab8-JamilTurpo/
RUN dotnet restore Lab8-JamilTurpo.sln

# Copiamos todo el proyecto
COPY . .

# Construimos la app en modo Release
RUN dotnet publish Lab8-JamilTurpo/Lab8-JamilTurpo.csproj -c Release -o /app/publish


# ====== RUNTIME STAGE ======
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copiar archivos publicados
COPY --from=build /app/publish .

# Exponer puerto Render usa EXPOSE 10000 o 5000 según necesite
EXPOSE 8080

# Necesario para Render (usa PORT variable)
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

ENTRYPOINT ["dotnet", "Lab8-JamilTurpo.dll"]
