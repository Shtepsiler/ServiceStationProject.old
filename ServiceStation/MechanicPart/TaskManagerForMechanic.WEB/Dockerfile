#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MechanicPart/TaskManagerForMechanic.WEB/TaskManagerForMechanic.WEB.csproj", "MechanicPart/TaskManagerForMechanic.WEB/"]
COPY ["MechanicPart/TaskManagerForMechanic.DAL/TaskManagerForMechanic.DAL.csproj", "MechanicPart/TaskManagerForMechanic.DAL/"]
RUN dotnet restore "MechanicPart/TaskManagerForMechanic.WEB/TaskManagerForMechanic.WEB.csproj"
COPY . .
WORKDIR "/src/MechanicPart/TaskManagerForMechanic.WEB"
RUN dotnet build "TaskManagerForMechanic.WEB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskManagerForMechanic.WEB.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskManagerForMechanic.WEB.dll"]