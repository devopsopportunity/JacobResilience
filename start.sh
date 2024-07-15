#!/bin/bash

# start.sh
# @ Edoardo Sabatini & ChatGPT 3.5 - Jul 2024
# -------------------------------------------------------------
# This script initializes the Jacob's Resilience game application.
# It resizes the terminal to 40 rows and 106 columns, clears the
# screen, runs the .NET application, and restores the normal cursor
# after the application exits.
# -------------------------------------------------------------
# @hacktlon July 15, 2024
# -------------------------------------------------------------

# Resize the terminal to 40 rows and 106 columns
resize -s 40 106

# Clear the terminal screen
clear

# Run the .NET application
dotnet run

# Restore normal cursor after application exits
tput cnorm
