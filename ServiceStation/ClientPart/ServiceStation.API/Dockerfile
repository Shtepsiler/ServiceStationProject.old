#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ServiceStation.API/ServiceStation.API.csproj", "ServiceStation.API/"]
COPY ["ServiceStation.BLL/ServiceStation.BLL.csproj", "ServiceStation.BLL/"]
COPY ["ServiceStation.DAL/ServiceStation.DAL.csproj", "ServiceStation.DAL/"]
RUN dotnet restore "ServiceStation.API/ServiceStation.API.csproj"
COPY . .
WORKDIR "/src/ServiceStation.API"
RUN dotnet build "ServiceStation.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServiceStation.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceStation.API.dll"]