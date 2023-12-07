using SchoolMusic.DataProcesor.ImportDTO;
using SchoolMusic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMusic.DataProcesor
{
    public class Deserializer
    {
        public static Dictionary<TimeOnly, Song> Deserialize(string listName)
        {
            XmlHelper helper = new XmlHelper();
            Dictionary<TimeOnly, Song> schedule = new Dictionary<TimeOnly, Song>();

            string schedulesString = File.ReadAllText(@$".\Playlists\{listName}.xml");

            ImportScheduleDto[] scheduleDtos = helper.Deserialize<ImportScheduleDto[]>(schedulesString, listName);

            foreach (var dto in scheduleDtos)
            {
                TimeOnly startingTime = TimeOnly.Parse(dto.StartingTime);

                Song song = new Song()
                     { 
                        Number = dto.SongNumber,
                        DurationInSecounds = dto.SongDurationInSecounds
                     };

                schedule.Add(startingTime, song);
            }

            return schedule;
        }
    }
}
