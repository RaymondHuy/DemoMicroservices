#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["DemoMicroservices.ToDoList.CommandApi/DemoMicroservices.ToDoList.CommandApi.csproj", "DemoMicroservices.ToDoList.CommandApi/"]
RUN dotnet restore "DemoMicroservices.ToDoList.CommandApi/DemoMicroservices.ToDoList.CommandApi.csproj"
COPY . .
WORKDIR "/src/DemoMicroservices.ToDoList.CommandApi"
RUN dotnet build "DemoMicroservices.ToDoList.CommandApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DemoMicroservices.ToDoList.CommandApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoMicroservices.ToDoList.CommandApi.dll"]