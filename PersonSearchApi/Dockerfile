# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy all backend files
COPY . .

# Restore dependencies and publish
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy published files from build stage
COPY --from=build /app/out .

# Set port for Render
ENV ASPNETCORE_URLS=http://+:$PORT
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "PersonSearchApi.dll"]
