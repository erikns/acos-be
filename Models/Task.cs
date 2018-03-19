using System.ComponentModel.DataAnnotations.Schema;

namespace ACOS_be.Models
{
    public class Task
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public User User { get; set; }
    }
}