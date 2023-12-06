using SchoolMusic;
using System;
using System.Globalization;
using System.Media;
using System.Reflection.Metadata.Ecma335;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("Въведе бройката на песните, които ще се стартират.");
        int count = int.Parse(Console.ReadLine());
        Dictionary<TimeOnly, Song> musicSchedule = SetSchedule(count);
        musicSchedule = musicSchedule.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

        PlaySchedule(musicSchedule);
    }

    private static void PlaySong(int number, int duration)
    {
        WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        player.URL = @$"Music\{number}.mp3";
        player.controls.play();
        TimeOnly timestamp = TimeOnly.FromDateTime(DateTime.Now.AddSeconds(duration));

        while (true) 
        {
            //string command = Console.ReadLine();

            //if (command.ToLower() == "stop")
            //{
            //    player.controls.stop();
            //    break;
            //}

            if (TimeOnly.FromDateTime(DateTime.Now) >= timestamp)
            {
                break;
            }

        }

    }

    private static Dictionary<TimeOnly, Song> SetSchedule(int count)
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

    private static void PlaySchedule(Dictionary<TimeOnly, Song> schedule)
    {
        for (int i = 0; i < schedule.Count;)
        {
            TimeOnly dateTime = schedule.ElementAt(i).Key;
            Song song = schedule.ElementAt(i).Value;

            while (true)
            {
                if (TimeOnly.FromDateTime(DateTime.Now) >= dateTime)
                {
                    PlaySong(song.Number, song.DurationInSecounds);

                    i++;
                    break;
                }
                
                System.Threading.Thread.Sleep(1000);
            }

        }
    }
}