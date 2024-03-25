#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Container we use for final publish
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5672

# Build container
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

# Copy the code into the container
WORKDIR /src
COPY ["src/ASNAOrders.Web/ASNAOrders.Web.csproj", "ASNAOrders.Web/"]

# NuGet restore
RUN dotnet restore "ASNAOrders.Web/ASNAOrders.Web.csproj"
COPY ["src/ASNAOrders.Web/", "ASNAOrders.Web/"]

# Build the API
WORKDIR "ASNAOrders.Web"
RUN dotnet build "ASNAOrders.Web.csproj" -c Release -o /app/build

# Publish it
FROM build AS publish
RUN dotnet publish "ASNAOrders.Web.csproj" -c Release -o /app/publish

# Make the final image for publishing
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ASNAOrders.Web.dll"]
