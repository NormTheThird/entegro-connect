#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EntegroConnect.API/EntegroConnect.API.csproj", "EntegroConnect.API/"]
RUN dotnet restore "./EntegroConnect.API/EntegroConnect.API.csproj"
COPY . .
WORKDIR "/src/EntegroConnect.API"
RUN dotnet build "EntegroConnect.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EntegroConnect.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EntegroConnect.API.dll"]