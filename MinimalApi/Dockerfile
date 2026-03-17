# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "MinimalApi/MinimalApi.csproj"
RUN dotnet publish "MinimalApi/MinimalApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# HTTPS certificate
COPY MinimalApi/Certs/localhost.pfx /app/localhost.pfx

# Kestrel settings for HTTP + HTTPS
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_URLS=https://+:443;http://+:8080
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/app/localhost.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=0e9bf06d-1373-4780-877a-a63833bf248b

ENTRYPOINT ["dotnet", "MinimalApi.dll"]