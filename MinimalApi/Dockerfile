# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy everything and restore/build
COPY . .
RUN dotnet restore "MinimalApi/MinimalApi.csproj"
RUN dotnet build "MinimalApi/MinimalApi.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet publish "MinimalApi/MinimalApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MinimalApi.dll"]