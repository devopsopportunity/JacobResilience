/*
 * AudioManager.cs
 * @authors Edoardo Sabatini & ChatGPT
 * -------------------------------------------------------------
 * This file defines the AudioManager class for managing background music.
 * It handles asynchronous playback of a music file in a loop using external processes,
 * ensuring smooth execution without blocking the main thread.
 * The music is played using the `play` command from `sox` to manage looping.
 * -------------------------------------------------------------
 * @hacktlon July 22, 2024
 */
using System.Diagnostics;
using Config;
using JacobResilienceGame;

namespace Modules
{
    public class AudioManager
    {
        private Thread? _musicThread;
        private readonly string _musicFilePath;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private Process? _process;
        
        /// <summary>
        /// Initializes a new instance of the AudioManager class.
        /// </summary>
        /// <param name="musicFilePath">The path to the music file to play in loop.</param>
        public AudioManager(string musicFilePath)
        {
            _musicFilePath = GameConfig.FOLDER_WAV + musicFilePath + ".wav";
        }

        /// <summary>
        /// Starts playing the background music in a loop asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task PlayBackgroundMusicAsync()
        {
            if(Program.audioOn)
            return Task.Run(() =>
            {
                _musicThread = new Thread(() =>
                {
                    var cancellationToken = _cancellationTokenSource.Token;
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        _process = new Process
                        {
                            StartInfo = new ProcessStartInfo
                            {
                                FileName = "play",
                                Arguments = _musicFilePath,
                                RedirectStandardOutput = true,
                                RedirectStandardError = true,
                                UseShellExecute = false,
                                CreateNoWindow = true
                            }
                        };
                        try
                        {
                            _process.Start();
                            _process.WaitForExit();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error playing background music: {ex.Message}");
                        }
                        finally
                        {
                            _process?.Dispose();
                            _process = null;
                        }
                    }
                })
                {
                    IsBackground = true
                };

                _musicThread.Start();
            });
            
            return Task.CompletedTask;
        }

        /// <summary>
        /// Stops the background music if it is currently playing.
        /// </summary>
        public void StopBackgroundMusic()
        {
            if (_musicThread != null)
            {
                _cancellationTokenSource.Cancel();
                _process?.Kill();
                _musicThread.Join();
                _musicThread = null;
                _cancellationTokenSource = new CancellationTokenSource();
            }
        }
    }
}
