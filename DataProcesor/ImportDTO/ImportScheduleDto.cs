using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SchoolMusic.DataProcesor.ImportDTO
{
    [XmlType("Playlist")]
    public class ImportScheduleDto
    {
        [XmlElement]
        public string StartingTime { get; set; } = null!;

        [XmlElement]
        public int SongNumber { get; set; }

        [XmlElement]
        public int SongDurationInSecounds { get; set; }
    }
}
