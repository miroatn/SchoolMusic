using SchoolMusic;
using SchoolMusic.Common;
using SchoolMusic.DataProcesor;
using SchoolMusic.DataProcesor.ExportDTO;
using SchoolMusic.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
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

        while (true)
        {
            try
            {
                Console.WriteLine(Messages.StartingMenu);
                ConsoleKeyInfo decision = Console.ReadKey();
                Console.WriteLine();

                if (decision.Key == ConsoleKey.D1)
                {
                    schedule = Player.ChoosePlaylist();
                }
                else if (decision.Key == ConsoleKey.D2)
                {
                    schedule = Player.CreatePlaylist(schedule);
                }
                else
                {
                    throw new Exception();
                }

                Player.PlaySchedule(schedule);
                Console.WriteLine(Messages.EndMessage);
                break;
            }
            catch (Exception)
            {
                Console.WriteLine(Messages.WrongInput);
                continue;
            }
        }

    }  

    

    
}