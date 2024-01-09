using SQLite;

namespace FinTrack.Models
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; } // You can store icon names or Unicode characters for icons
    }
}