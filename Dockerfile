FROM mcr.microsoft.com/dotnet/sdk:7.0 AS builder
WORKDIR /

COPY ./src/Echo.Api ./src

RUN dotnet restore ./src
RUN dotnet publish -c Release -p:PublishDir=/output ./src/Echo.Api.csproj 

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
WORKDIR /app
COPY --from=builder /output .
STOPSIGNAL SIGINT

ENV DOTNET_EnableDiagnostics=0
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://*:80
EXPOSE 80

ENTRYPOINT ["dotnet", "Echo.Api.dll"]