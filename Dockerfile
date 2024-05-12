FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/BookLibrary.Api/BookLibrary.Api.csproj", "src/BookLibrary.Api/"]
COPY ["src/BookLibrary.Application/BookLibrary.Application.csproj", "src/BookLibrary.Application/"]
COPY ["src/BookLibrary.Core/BookLibrary.Core.csproj", "src/BookLibrary.Core/"]
COPY ["src/BookLibrary.Infrastructure/BookLibrary.Infrastructure.csproj", "src/BookLibrary.Infrastructure/"]

RUN dotnet restore "src/BookLibrary.Api/BookLibrary.Api.csproj"
COPY . .
WORKDIR "/src/src/BookLibrary.Api"
RUN dotnet build "BookLibrary.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BookLibrary.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookLibrary.Api.dll"]
