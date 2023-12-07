using SchoolMusic.DataProcesor.ExportDTO;
using SchoolMusic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMusic.DataProcesor
{
    public class Serializer
    {
        public static void SerializeSchedule(Dictionary<TimeOnly, Song> musicSchedule, string name)
        {
            //Serialize schedule to XML file

            XmlHelper helper = new XmlHelper();
            List<ExportScheduleDTO> list = new List<ExportScheduleDTO>();

            foreach (var song in musicSchedule)
            {
                ExportScheduleDTO exportDTO = new ExportScheduleDTO()
                {
                    StartingTime = song.Key.ToString("HH':'mm':'ss"),
                    SongNumber = song.Value.Number,
                    SongDurationInSecounds = song.Value.DurationInSecounds
                };

                list.Add(exportDTO);
            }

            string result = helper.Serialize(list, name);
            File.WriteAllText(@$"Playlists/{name}.xml", result);
        }
    }
}
