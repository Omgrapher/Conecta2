# Imagen base para ejecutar la app en modo rápido (Debug)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Usar la imagen base de .NET SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar el archivo de proyecto desde la subcarpeta 'conecta2'
COPY conecta2/conecta2.csproj conecta2/
RUN dotnet restore "conecta2/conecta2.csproj"

# Copiar el resto del código
COPY . .

# Establecer el directorio de trabajo y construir la aplicación
WORKDIR /src/conecta2
RUN dotnet build -c Release -o /app/build

# Publicar la aplicación en el contenedor
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Generar la imagen final para ejecutar la app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "conecta2.dll"]
