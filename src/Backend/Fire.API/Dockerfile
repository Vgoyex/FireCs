# -------- BUILD --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia toda a pasta Backend
COPY src/Backend/ ./Backend/

# Vai para a pasta do projeto principal
WORKDIR /app/Backend/Fire.API

# Restore e publish
RUN dotnet restore FireCs.sln
RUN dotnet publish Fire.API.csproj -c Release -o /app/publish

# -------- RUNTIME --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "Fire.API.dll"]
