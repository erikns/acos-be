using System.ComponentModel.DataAnnotations.Schema;

namespace ACOS_be.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
