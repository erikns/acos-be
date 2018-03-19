namespace ACOS_be.Models
{
    public class Task
    {
        int Id { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        User User { get; set; }
    }
}