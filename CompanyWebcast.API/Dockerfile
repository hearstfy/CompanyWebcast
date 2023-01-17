#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CompanyWebcast.API/CompanyWebcast.API.csproj", "CompanyWebcast.API/"]
COPY ["CompanyWebcast.Infrastructure/CompanyWebcast.Infrastructure.csproj", "CompanyWebcast.Infrastructure/"]
COPY ["CompanyWebcast.Application/CompanyWebcast.Application.csproj", "CompanyWebcast.Application/"]
COPY ["CompanyWebcast.Domain/CompanyWebcast.Domain.csproj", "CompanyWebcast.Domain/"]
RUN dotnet restore "CompanyWebcast.API/CompanyWebcast.API.csproj"
COPY . .
WORKDIR "/src/CompanyWebcast.API"
RUN dotnet build "CompanyWebcast.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CompanyWebcast.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CompanyWebcast.API.dll"]