FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["management.Api/management.Api.csproj", "management.Api/"]
COPY ["management.Application/management.Application.csproj", "management.Application/"]
COPY ["management.Infrastructure/management.Infrastructure.csproj", "management.Infrastructure/"]
COPY ["management.Domain/management.Domain.csproj", "management.Domain/"]

RUN dotnet restore "management.Api/management.Api.csproj"

COPY . .

WORKDIR /src/management.Api

RUN dotnet build "management.Api.csproj" -c Release -o /app/build

FROM build AS publish

WORKDIR /src/management.Api

RUN dotnet publish "management.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=publish /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "management.Api.dll"]
