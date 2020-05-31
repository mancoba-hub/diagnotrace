using SQLite;

namespace DiagnoTrace.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        [MaxLength(12)]
        public string Password { get; set; }

        public User()
        {

        }
    }
}
