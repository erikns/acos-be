using System.ComponentModel.DataAnnotations.Schema;

namespace ACOS_be.Entities
{
    public class Task
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public TaskType Type { get; set; }
    }
}