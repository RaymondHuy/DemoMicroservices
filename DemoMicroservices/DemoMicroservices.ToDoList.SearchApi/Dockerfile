#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["DemoMicroservices.ToDoList.SearchApi/DemoMicroservices.ToDoList.SearchApi.csproj", "DemoMicroservices.ToDoList.SearchApi/"]
RUN dotnet restore "DemoMicroservices.ToDoList.SearchApi/DemoMicroservices.ToDoList.SearchApi.csproj"
COPY . .
WORKDIR "/src/DemoMicroservices.ToDoList.SearchApi"
RUN dotnet build "DemoMicroservices.ToDoList.SearchApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DemoMicroservices.ToDoList.SearchApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoMicroservices.ToDoList.SearchApi.dll"]