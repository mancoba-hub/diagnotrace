using SQLite;

namespace DiagnoTrace.Models
{
    public class Question
    {
        [PrimaryKey, AutoIncrement]
        public int QuestionId { get; set; }

        public string DeviceId { get; set; }

        public string QuestionText { get; set; }

        public string QuestionAnswer { get; set; }
    }
}
