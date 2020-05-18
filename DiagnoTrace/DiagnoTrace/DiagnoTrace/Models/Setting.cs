using SQLite;

namespace DiagnoTrace.Models
{
    public class Setting
    {
        [PrimaryKey, AutoIncrement]
        public int SettingId { get; set; }
        public string DeviceId { get; set; }
        public bool CanNotify { get; set; }
    }
}
