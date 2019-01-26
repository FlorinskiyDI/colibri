FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Survey.Webapi/Survey.Webapi.csproj Survey.Webapi/
COPY ../Survey.Common/Survey.Common.csproj ../Survey.Common/
COPY Survey.InfrastructureLayer/Survey.InfrastructureLayer.csproj Survey.InfrastructureLayer/
COPY Survey.DomainModelLayer/Survey.DomainModelLayer.csproj Survey.DomainModelLayer/
COPY Survey.ApplicationLayer/Survey.ApplicationLayer.csproj Survey.ApplicationLayer/
RUN dotnet restore Survey.Webapi/Survey.Webapi.csproj
COPY . .
WORKDIR /src/Survey.Webapi
RUN dotnet build Survey.Webapi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Survey.Webapi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Survey.Webapi.dll"]
