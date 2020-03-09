#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["DemoMicroservices.ApiGateway/DemoMicroservices.ApiGateway.csproj", "DemoMicroservices.ApiGateway/"]
RUN dotnet restore "DemoMicroservices.ApiGateway/DemoMicroservices.ApiGateway.csproj"
COPY . .
WORKDIR "/src/DemoMicroservices.ApiGateway"
RUN dotnet build "DemoMicroservices.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DemoMicroservices.ApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoMicroservices.ApiGateway.dll"]