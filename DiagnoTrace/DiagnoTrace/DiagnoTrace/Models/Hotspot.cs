using SQLite;

namespace DiagnoTrace.Models
{
    public class Hotspot
    {
        [PrimaryKey, AutoIncrement]
        public int HotspotId { get; set; }

        public string Province { get; set; }
        
        public string City { get; set; }

        public string Area { get; set; }
    }
}
