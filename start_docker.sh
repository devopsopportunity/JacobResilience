#!/bin/bash

# start_docker.sh
# @ Edoardo Sabatini & ChatGPT 3.5 - Jul 2024
# -------------------------------------------------------------
# This script builds and runs the 'jacob-resilience' Docker container for the
# Jacob's Resilience game application. It first builds the Docker
# image named 'jacob-resilience' using the Dockerfile in the current directory,
# then resizes the terminal, clears the screen, runs the 'jacob-resilience'
# Docker container interactively (removing it when it exits), and
# finally restores the normal cursor and lists Docker images.
# -------------------------------------------------------------
# @hacktlon July 15, 2024
# -------------------------------------------------------------

# Build the Docker image named 'jacob-resilience' using the Dockerfile in the current directory
docker build -t jacob-resilience -f Dockerfile .

# Resize the terminal to 40 rows and 106 columns
resize -s 40 106

# Clear the terminal screen
clear

# Run the 'jacob-resilience' Docker container interactively and remove it when it exits
docker run -it --rm jacob-resilience

# Restore normal cursor after application exits
tput cnorm

# List Docker images to verify 'jacob-resilience' image has been created
docker images
