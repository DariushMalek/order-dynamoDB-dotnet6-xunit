#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TechLand.Shop.Api/TechLand.Shop.Api.csproj", "TechLand.Shop.Api/"]
COPY ["TechLand.Shop.Data/TechLand.Shop.Data.csproj", "TechLand.Shop.Data/"]
COPY ["TechLand.Shop.Model/TechLand.Shop.Model.csproj", "TechLand.Shop.Model/"]
RUN dotnet restore "TechLand.Shop.Api/TechLand.Shop.Api.csproj"
COPY . .
WORKDIR "/src/TechLand.Shop.Api"
RUN dotnet build "TechLand.Shop.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TechLand.Shop.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechLand.Shop.Api.dll"]