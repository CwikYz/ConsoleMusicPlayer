using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Wave;
using NAudio.Lame;

namespace MusicPlayer
{
    class Program
    {
        static List<string> playlist = new List<string>();
        static int currentSongIndex = 0;
        static WaveOutEvent? outputDevice;
        static AudioFileReader? audioFile;

        static void Main()
        {
            Console.WriteLine("Enter commands (play, pause, next, prev, add, shuffle, quit):");
            while (true)
            {
                Console.Write("> ");
                string? cmd = Console.ReadLine();
                switch (cmd)
                {
                    case "play":
                        if (playlist.Count == 0)
                        {
                            Console.WriteLine("No songs in playlist.");
                            break;
                        }
                        PlaySong();
                        break;
                    case "pause":
                        Pause();
                        break;
                    case "resume":
                        Resume();
                        break;
                    case "next":
                        Next();
                        break;
                    case "prev":
                        Prev();
                        break;
                    case "add":
                        AddSong();
                        break;
                    case "shuffle":
                        Shuffle();
                        break;
                    case "quit":
                        Quit();
                        break;
                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
        }

        static void AddSong()
        {
            Console.WriteLine("Enter song location:");
            string? songLoc = Console.ReadLine();
            playlist.Add(songLoc);
            Console.WriteLine("Added: " + songLoc);
        }

        static void PlaySong()
        {
            audioFile = new AudioFileReader(playlist[currentSongIndex]);
            outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.Play();
            Console.WriteLine("Playing: " + playlist[currentSongIndex]);
        }

        static void Pause()
        {
            if (outputDevice != null)
            {
                outputDevice.Pause();
                Console.WriteLine("Paused.");
            }
        }

        static void Resume()
        {
            if (outputDevice != null)
            {
                outputDevice.Play();
                Console.WriteLine("Resumed.");
            }
        }

        static void Next()
        {
            currentSongIndex = (currentSongIndex + 1) % playlist.Count;
            PlaySong();
        }

        static void Prev()
        {
            currentSongIndex = (currentSongIndex + playlist.Count - 1) % playlist.Count;
            PlaySong();
        }

        static void Shuffle()
        {
            currentSongIndex = new Random().Next(playlist.Count);
            PlaySong();
        }

        static void Quit()
        {
            if (outputDevice != null)
            {
                outputDevice.Stop();
                outputDevice.Dispose();
            }
            Console.WriteLine("Quitting.");
            Environment.Exit(0);
        }
    }
}
