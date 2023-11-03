#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MauiLabs.Api/MauiLabs.Api.csproj", "MauiLabs.Api/"]
RUN dotnet restore "MauiLabs.Api/MauiLabs.Api.csproj"
COPY . .
WORKDIR "/src/MauiLabs.Api"
RUN dotnet build "MauiLabs.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MauiLabs.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MauiLabs.Api.dll"]