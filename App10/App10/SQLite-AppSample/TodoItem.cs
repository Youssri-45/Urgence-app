using SQLite;

namespace XamarinForms.SQLite
{
    public class TodoItem
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        
    }
}