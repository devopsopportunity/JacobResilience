# Jacob's Resilience CSHARP Linux Game Platform

Greetings of today!!! 😊 
This hot summer has inspired me to create this C# .NET 8 game, based on the true story of Jacob, the lion king of the Kazinga Channel.

## GamePlay Demo

Watch the gameplay video of "Jacob's Resilience" on YouTube:

[Watch on YouTube](https://youtu.be/cRSOrJi2yA4)

## Overview

Jacob's Resilience is a C# .NET 8 game that takes inspiration from the true story of Jacob, the lion king of the Kazinga Channel in Uganda. This game encapsulates Jacob's daring journey across treacherous waters, facing challenges such as crocodiles hippos and poachers' traps, symbolizing his resilience in the face of adversity, all with emojis.

## Getting Started

To get started with Jacob's Resilience on your system, follow these steps:

1. **Install Dependencies:**
   - Ensure you have `xterm` and `sox` installed:
     ```bash
     sudo apt-get install xterm
     sudo apt install sox
     ```

2. **Start the Application:**
   - Run the start script for regular application mode:
     ```bash
     ./start.sh application
     ```

   - Alternatively, use Docker for deployment:
     ```bash
     ./start_docker.sh
     ```

3. **Clean Build Files:**
   - Use the following script to clean up build files generated by `dotnet`:
     ```bash
     ./clean_build_files.sh
     ```

4. **Build and Release:**
   - Build the application without executing:
     ```bash
     dotnet build
     ```

   - Release the application for Windows (generate `.exe` file):
     ```bash
     dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true ./csproj/DotNet.Docker.csproj
     ```
     
