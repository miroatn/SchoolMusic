using SchoolMusic.DataProcesor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMusic
{
    public class Player
    {
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

        }

        public static Dictionary<TimeOnly, Song> SetSchedule(int count)
        {
            const string error = "Имате грешка във формата на въведения час! Моля опитайте отново.";

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
                        Console.WriteLine("Моля въведете часа, в който искате да се стартира песента във формат час:минути:секунди. Пример: 10:21:00");
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
                        break;

                    }
                    catch (Exception)
                    {
                        Console.WriteLine(error);
                        continue;
                    }

                }

                Console.WriteLine("Моля въведете номера на песента и продължителността ѝ в секунди. Пример 1,120");

                string[] numberAndLength = Console.ReadLine().Split(',');

                int number = int.Parse(numberAndLength[0]);
                int length = int.Parse(numberAndLength[1]);

                song.Number = number;
                song.DurationInSecounds = length;

                if (!result.Keys.Contains(dateTime))
                {
                    result.Add(dateTime, song);
                }

            }

            return result;
        }

        public static void PlaySchedule(Dictionary<TimeOnly, Song> schedule)
        {
            for (int i = 0, j = 0, k = 0; i < schedule.Count;)
            {
                TimeOnly dateTime = schedule.ElementAt(i).Key;
                Song song = schedule.ElementAt(i).Value;

                while (true)
                {

                    if (dateTime.Minute < TimeOnly.FromDateTime(DateTime.Now).Minute ||
                        dateTime.Hour < TimeOnly.FromDateTime(DateTime.Now).Hour)
                    {
                        i++;
                        break;
                    }

                    if (j == k)
                    {
                        Console.WriteLine($"Следващата песен ще започне в {dateTime}.");
                        k++;
                    }

                    if (TimeOnly.FromDateTime(DateTime.Now) >= dateTime)
                    {
                        PlaySong(song.Number, song.DurationInSecounds);

                        i++;
                        j++;
                        break;
                    }

                    System.Threading.Thread.Sleep(1000);
                }

            }
        }

        public static void CreatePlaylist(Dictionary<TimeOnly, Song> schedule)
        {
            Console.WriteLine("Въведе бройката на песните, които ще се стартират.");
            int count = int.Parse(Console.ReadLine());
            schedule = SetSchedule(count);
            schedule = schedule.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);


            //Saving the playlist in XML file.
            Console.WriteLine("Ако желаете този плейлист да бъде запазен натиснете 1, в противен случай натиснете 2");
            int answer = int.Parse(Console.ReadLine());

            if (answer == 1)
            {
                Console.WriteLine("Въведете име на плейлиста.\r\n" +
                " !!! Имайте в предвид, че ако въведете име на вече съществуващ плейлист, то новият ще се запише на мястото на стария!");

                string playlistName = Console.ReadLine();

                Serializer.SerializeSchedule(schedule, playlistName);
                Console.WriteLine($"Плейлист с име {playlistName} беше успешно създаден!");
            }
        }


    }
}
