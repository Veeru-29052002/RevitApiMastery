# Base runtime stage using official .NET 10 ASP.NET Core image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

# Build stage using official .NET 10 SDK image
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project file and restore dependencies first to leverage Docker layer caching
COPY ["RevitApiMastery.csproj", "./"]
RUN dotnet restore "RevitApiMastery.csproj"

# Copy all source files and build
COPY . .
WORKDIR "/src"
RUN dotnet build "RevitApiMastery.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RevitApiMastery.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final production stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RevitApiMastery.dll"]
