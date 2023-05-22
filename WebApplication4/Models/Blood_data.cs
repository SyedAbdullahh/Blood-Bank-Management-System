
using System.ComponentModel.DataAnnotations;
namespace WebApplication4.Models
{
    public class Blood_data
    {
        [Key]
        public int b_Id { get; set; }
        [Required]
        public int h_id { get; set; }
        [Required]
        public string b_type { get; set;}
        [Required]
        public int b_quantity { get; set;}
        [Required]
        public int b_price { get; set;}

    }
}
