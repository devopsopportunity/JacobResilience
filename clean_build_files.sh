#!/bin/bash

# clean_build_files.sh
# @authors Edoardo Sabatini & ChatGPT 3.5
# -------------------------------------------------------------
# This script cleans up build artifacts for the project.
# It removes the 'bin' and 'obj' directories recursively.
# -------------------------------------------------------------
# @hacktlon July 15, 2024

rm -rf bin
rm -rf obj
echo "Cleaning done."