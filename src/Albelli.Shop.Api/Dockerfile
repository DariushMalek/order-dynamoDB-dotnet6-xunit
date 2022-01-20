#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Albelli.Shop.Api/Albelli.Shop.Api.csproj", "Albelli.Shop.Api/"]
COPY ["Albelli.Shop.Data/Albelli.Shop.Data.csproj", "Albelli.Shop.Data/"]
COPY ["Albelli.Shop.Model/Albelli.Shop.Model.csproj", "Albelli.Shop.Model/"]
RUN dotnet restore "Albelli.Shop.Api/Albelli.Shop.Api.csproj"
COPY . .
WORKDIR "/src/Albelli.Shop.Api"
RUN dotnet build "Albelli.Shop.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Albelli.Shop.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Albelli.Shop.Api.dll"]