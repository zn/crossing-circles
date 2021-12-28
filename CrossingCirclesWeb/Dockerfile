FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CrossingCirclesWeb/CrossingCirclesWeb.csproj", "CrossingCirclesWeb/"]
RUN dotnet restore "CrossingCirclesWeb/CrossingCirclesWeb.csproj"
COPY . .
WORKDIR "/src/CrossingCirclesWeb"
RUN dotnet build "CrossingCirclesWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CrossingCirclesWeb.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CrossingCirclesWeb.dll"]
