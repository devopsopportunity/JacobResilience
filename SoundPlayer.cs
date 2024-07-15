/*
 * SoundPlayer.cs
 * @authors Edoardo Sabatini & ChatGPT 3.5
 * -------------------------------------------------------------
 * This file defines the SoundPlayer class for managing game sound.
 * It handles asynchronous playback of sound files using a queue,
 * ensuring smooth execution without blocking the main thread.
 * Sound files are played using external processes to leverage system
 * capabilities and manage playback exceptions effectively.
 * -------------------------------------------------------------
 * @hacktlon July 15, 2024
 */
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Modules
{
    public class SoundPlayer
    {
        private readonly ConcurrentQueue<string> soundQueue = new ConcurrentQueue<string>();
        private bool isProcessing = false;

        /// <summary>
        /// Enqueues a sound file for asynchronous playback.
        /// </summary>
        /// <param name="fileName">The name of the sound file to play.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task PlayAsync(string fileName)
        {
            soundQueue.Enqueue(fileName);
            if (!isProcessing)
            {
                isProcessing = true;
                return Task.Run(ProcessSoundQueue);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Processes the sound queue asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task ProcessSoundQueue()
        {
            while (soundQueue.TryDequeue(out string? fileName))
            {
                if (fileName != null)
                {
                    await Task.Run(() => Play(fileName));
                }
            }
            isProcessing = false;
        }

        /// <summary>
        /// Plays a sound file using an external process.
        /// </summary>
        /// <param name="fileName">The name of the sound file to play.</param>
        private void Play(string fileName)
        {
            RunProcess("play", fileName + ".wav");
        }

        /// <summary>
        /// Executes a process to play a sound file.
        /// </summary>
        /// <param name="fileName">The name of the executable or command.</param>
        /// <param name="arg">Arguments for the process.</param>
        private void RunProcess(string fileName, string arg)
        {
            try
            {
                using (var process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = fileName,
                        Arguments = arg,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    };

                    process.Start();
                    process.WaitForExit(100); // Wait for a maximum of 100ms
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during sound playback: {ex.Message}");
            }
        }
    }
}
