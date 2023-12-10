using SchoolMusic.Common;
using SchoolMusic.DataProcesor;
using SchoolMusic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMusic
{
    public class Player
    {
        public static Dictionary<TimeOnly, Song> ChoosePlaylist()
        {
            while (true)
            {
                string[] playLists = Directory.GetFiles(@".\Playlists");
                Console.WriteLine(Messages.ChooseList);

                Dictionary<int, string> availablePlaylists = new();

                for (int i = 1; i <= playLists.Length; i++)
                {
                    string playListName = Path.GetFileNameWithoutExtension(playLists[i - 1]);
                    availablePlaylists.Add(i, playListName);
                    Console.WriteLine($"{i}. {playListName}");
                }

                ConsoleKeyInfo chosenNumber = Console.ReadKey();
                Console.WriteLine();

                if (!char.IsDigit(chosenNumber.KeyChar))
                {
                    Console.WriteLine(Messages.InvalidNumber);
                    continue;
                }

                string chosenPlaylist = availablePlaylists.FirstOrDefault(ap => ap.Key == int.Parse(chosenNumber.KeyChar.ToString())).Value;

                if (chosenPlaylist == null)
                {
                    Console.WriteLine(Messages.InvalidNumber);
                    continue;
                }
                else
                {
                    return Deserializer.Deserialize(chosenPlaylist);
                }
            }
        }

        public static void PlaySong(int number, int duration)
        {
            WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
            player.URL = @$"Music\{number}.mp3";
            player.controls.play();
            TimeOnly timestamp = TimeOnly.FromDateTime(DateTime.Now.AddSeconds(duration));

            while (true)
            {

                if (TimeOnly.FromDateTime(DateTime.Now) >= timestamp)
                {
                    break;
                }

            }

            player.controls.stop();

        }

        public static void PlaySchedule(Dictionary<TimeOnly, Song> schedule)
        {
            bool isReady = false;
            bool isPrinted = false;

            for (int i = 0; i < schedule.Count;)
            {
                TimeOnly dateTime = schedule.ElementAt(i).Key;
                Song song = schedule.ElementAt(i).Value;

                if (dateTime < TimeOnly.FromDateTime(DateTime.Now) && isReady == false)
                {
                    i++;
                    continue;
                }

                isReady = true;

                while (true)
                {

                    if (!isPrinted)
                    {
                        Console.WriteLine(string.Format(Messages.NextSongStartingTime, dateTime.ToString("HH:mm:ss")));
                        isPrinted = true;
                    }

                    if (TimeOnly.FromDateTime(DateTime.Now) >= dateTime)
                    {
                        PlaySong(song.Number, song.DurationInSecounds);

                        i++;
                        isPrinted = false;
                        break;
                    }

                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        public static Dictionary<TimeOnly, Song> CreatePlaylist(Dictionary<TimeOnly, Song> schedule)
        {
          
            while (true)
            {
                try
                {
                    Console.WriteLine(Messages.SongsCountMsg);
                    int count = int.Parse(Console.ReadLine());
                    schedule = SetSchedule(count);
                    schedule = schedule.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

                    //Saving the playlist in XML file.
                    Console.WriteLine(Messages.SavePlaylistMsg);

                    ConsoleKeyInfo decision = Console.ReadKey();
                    Console.WriteLine();

                    if (decision.Key == ConsoleKey.D1)
                    {
                        while (true)
                        {
                            Console.WriteLine(Messages.NamingPlaylistMsg);

                            string playlistName = Console.ReadLine();

                            string[] playLists = Directory.GetFiles(@".\Playlists");
                            List<string> convertedPlaylist = new List<string>();

                            for (int i = 0; i < playLists.Length; i++)
                            {
                                string currentPlayList = Path.GetFileNameWithoutExtension(playLists[i]);
                                convertedPlaylist.Add(currentPlayList);
                            }

                            if (convertedPlaylist.Any(cp => cp == playlistName))
                            {
                                Console.WriteLine(Messages.ExistingPlaylistName);
                                continue;
                            }

                            Serializer.SerializeSchedule(schedule, playlistName);
                            Console.WriteLine(string.Format(Messages.SuccessfullyCreatedPlaylist, playlistName));
                            break;
                        }
                    }

                    return schedule;
                }
                catch (Exception)
                {
                    Console.WriteLine(Messages.WrongInput);
                    continue;
                }
            }

        }

        public static Dictionary<TimeOnly, Song> SetSchedule(int count)
        {

            Dictionary<TimeOnly, Song> result = new();

            for (int i = 0; i < count; i++)
            {
                TimeOnly dateTime = new();
                Song song = new Song();
                // Adding the starting time for song
                while (true)
                {
                    try
                    {
                        Console.WriteLine(Messages.StartingHourMsg);
                        string[] input = Console.ReadLine().Split(':');
                        int hours = int.Parse(input[0]);
                        int minutes = int.Parse(input[1]);
                        int seconds = int.Parse(input[2]);

                        if (input.Length == 0 ||
                           (hours > 24 || hours < 0) ||
                           (minutes > 60 || minutes < 0) ||
                           (seconds > 60 || seconds < 0))
                        {
                            throw new Exception();
                        }

                        dateTime = new TimeOnly(hours, minutes, seconds);

                        if (result.Keys.Contains(dateTime))
                        {
                            Console.WriteLine(Messages.TimingAlreadyExist);
                            continue;
                        }

                        break;

                    }
                    catch (Exception)
                    {
                        Console.WriteLine(Messages.WrongHourError);
                        continue;
                    }

                }

                while (true)
                {
                    try
                    {
                        Console.WriteLine(Messages.SongNumberAndLengthMsg);

                        string[] numberAndLength = Console.ReadLine().Split(',');

                        int number = int.Parse(numberAndLength[0]);
                        int length = int.Parse(numberAndLength[1]);
                        song.Number = number;
                        song.DurationInSecounds = length;

                        if (!result.Keys.Contains(dateTime))
                        {
                            result.Add(dateTime, song);

                            // Printing next free timing for a song
                            double dLength = double.Parse(numberAndLength[1]);
                            TimeOnly nextFreeTiming = TimeOnlyExtensions.AddSeconds(dateTime, dLength);
                            Console.WriteLine(string.Format(Messages.NextAvaliableTiming, nextFreeTiming.ToString("HH:mm:ss")));
                            break;
                        }

                    }
                    catch (Exception)
                    {
                        Console.WriteLine(Messages.WrongNumberOrLength);
                        continue;
                    }
                }

            }

            return result;
        }

    }
}
