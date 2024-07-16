# Dockerfile for building and running the DotNet.Docker application
# @authors Edoardo Sabatini & ChatGPT 3.5
# -------------------------------------------------------------
# This Dockerfile defines the multi-stage build process for the DotNet.Docker application.
# It first builds the application using the .NET SDK and then packages it into a runtime image
# based on the ASP.NET runtime.
# -------------------------------------------------------------
# @hacktlon July 15, 2024

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./

# Install play command line
RUN apt-get update && apt-get install -y sox && rm -rf /var/lib/apt/lists/*

# Verify sox installation and check if play command is available
RUN sox --version && which play

# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App

# Copy the output from the build stage
COPY --from=build-env /App/out .
COPY --from=build-env /usr/bin/play /usr/bin/play
COPY --from=build-env /usr/bin/sox /usr/bin/sox

ENTRYPOINT ["dotnet", "JacobResilience.dll"]
