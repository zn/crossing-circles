FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY "CrossingCirclesWeb.sln" .
COPY "CrossingCirclesWeb/CrossingCirclesWeb.csproj" "./CrossingCirclesWeb/"
COPY "Models/Models.csproj" "./Models/"
COPY "Tests/Tests.csproj" "./Tests/"
RUN dotnet restore
COPY . .
WORKDIR /src/CrossingCirclesWeb
RUN dotnet build "CrossingCirclesWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CrossingCirclesWeb.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CrossingCirclesWeb.dll"]
