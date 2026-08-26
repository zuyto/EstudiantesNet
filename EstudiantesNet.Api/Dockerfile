FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base

USER root

RUN apk update && \
    apk upgrade && \
    apk add --no-cache icu-libs tzdata

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

USER $APP_UID

WORKDIR /app

EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY ["EstudiantesNet.Api/EstudiantesNet.Api.csproj", "EstudiantesNet.Api/"]
COPY ["EstudiantesNet.Application/EstudiantesNet.Application.csproj", "EstudiantesNet.Application/"]
COPY ["EstudiantesNet.Domain/EstudiantesNet.Domain.csproj", "EstudiantesNet.Domain/"]
COPY ["EstudiantesNet.Logger/EstudiantesNet.Logger.csproj", "EstudiantesNet.Logger/"]
COPY ["EstudiantesNet.Infrastructure/EstudiantesNet.Infrastructure.csproj", "EstudiantesNet.Infrastructure/"]

RUN dotnet restore "./EstudiantesNet.Api/EstudiantesNet.Api.csproj"

COPY . .

WORKDIR "/src/EstudiantesNet.Api"

RUN dotnet build "./EstudiantesNet.Api.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/build


FROM build AS publish

ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "./EstudiantesNet.Api.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false


FROM base AS final

WORKDIR /app

ENV ASPNETCORE_HTTP_PORTS=8080

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "EstudiantesNet.Api.dll"]
