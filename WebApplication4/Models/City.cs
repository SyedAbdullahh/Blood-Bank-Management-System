using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class City
    {
        [Key]
        public int c_id{ get; set; }
        [Required]
        public string c_name { get; set; }
    }
}
