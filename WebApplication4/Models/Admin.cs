using System.ComponentModel.DataAnnotations;
namespace WebApplication4.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required] 
        
        public string password { get; set; }
    }
}
