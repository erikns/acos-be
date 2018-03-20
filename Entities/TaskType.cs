using System.ComponentModel.DataAnnotations.Schema;

namespace ACOS_be.Entities
{
    public class TaskType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}