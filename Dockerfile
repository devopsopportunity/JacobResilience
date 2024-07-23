# Dockerfile for building and running the DotNet.Docker application
# @authors Edoardo Sabatini & ChatGPT 3.5
# -------------------------------------------------------------
# This Dockerfile defines the process for building and running the DotNet.Docker application.
# It uses the .NET SDK image to build and run the application.
# -------------------------------------------------------------
# @Hackathon July 13th to 23rd, 2024

# Use the .NET SDK image for build and run
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything to the container
COPY . ./

# Install necessary packages
RUN apt-get update && apt-get install -y sox && rm -rf /var/lib/apt/lists/*

# Verify sox installation and check if play command is available
RUN sox --version && which play

# Restore dependencies
RUN dotnet restore

# Set the entry point for the container to run the .NET application using dotnet run
ENTRYPOINT ["dotnet", "run", "--project", "JacobResilience.csproj"]
