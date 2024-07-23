#!/bin/bash

# start.sh
# @ Edoardo Sabatini & ChatGPT 3.5 - Jul 2024
# -------------------------------------------------------------
# This script initializes the Jacob's Resilience game application.
# It resizes the terminal to 40 rows and 106 columns, clears the
# screen, runs the .NET application, and restores the normal cursor
# after the application exits.
# -------------------------------------------------------------
# @Hackathon July 13th to 23rd, 2024
# -------------------------------------------------------------

export SCREEN_HEIGHT=40
export SCREEN_WIDTH=106

# Resize the terminal to 40 rows and 106 columns
resize -s $SCREEN_HEIGHT $SCREEN_WIDTH

# Clear the terminal screen
clear

# Run the .NET application
dotnet run JacobResilience.csproj

# Restore normal cursor after application exits
tput cnorm
