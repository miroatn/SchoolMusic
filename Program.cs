using SchoolMusic;
using SchoolMusic.DataProcesor;
using SchoolMusic.DataProcesor.ExportDTO;
using SchoolMusic.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Media;
using System.Reflection.Metadata.Ecma335;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine(LogoMaker.Make());
        Console.WriteLine();

        Console.OutputEncoding = Encoding.UTF8;
        Dictionary<TimeOnly, Song> schedule = new();

        Console.WriteLine("Ако искате да използвате готов плейлист натиснете 1.\r\nАко искате да създадете нов плейлист натиснете 2.");

        int decision = int.Parse(Console.ReadLine());

        if (decision == 1)
        {
            string[] playLists = Directory.GetFiles(@".\Playlists");
            Console.WriteLine("Изберете от списъка с плейлисти и въведете името на плейлиста, който желаете да се стартира.");

            for (int i = 0; i < playLists.Length; i++)
            {
                string playList = Path.GetFileNameWithoutExtension(playLists[i]);

                Console.WriteLine($"{i+1}. {playList}");
            }

            string playlistName = Console.ReadLine();
            schedule = Deserializer.Deserialize(playlistName);
        }
        else if (decision == 2)
        {
           schedule = Player.CreatePlaylist(schedule);
        }


        Player.PlaySchedule(schedule);
        Console.WriteLine("Всички песни бяха изпълнени за деня!");
    }  

    

    
}