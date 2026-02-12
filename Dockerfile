# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Global.sln ./
COPY src/Global.Host/Global.Host.csproj src/Global.Host/
COPY src/Global.Manager/Global.Manager.csproj src/Global.Manager/
COPY src/Global.Access/Global.Access.csproj src/Global.Access/

RUN dotnet restore ./Global.sln

COPY src/ ./src/

RUN dotnet publish src/Global.Host/Global.Host.csproj -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

COPY --from=build /app/publish ./

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Global.Host.dll"]
