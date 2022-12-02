using SQLite;

namespace NotificationToDo.Models
{
    public class TodoItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Note { get; set; }
    }

}