using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SchoolMusic.DataProcesor.ExportDTO
{
    [XmlType("Playlist")]
    public class ExportScheduleDTO
    {
        [XmlElement]
        public string StartingTime { get; set; } = null!;

        [XmlElement]
        public int SongNumber { get; set; }

        [XmlElement]
        public int SongDurationInSecounds { get; set; }
    }
}
