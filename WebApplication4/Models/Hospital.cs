
using System.ComponentModel.DataAnnotations;
namespace WebApplication4.Models
{
    public class Hospital
    {
        [Key]
        public int h_Id { get; set; }
        [Required]  
        public string h_name { get; set; }
        [Required]
        public  string h_city { get; set; }
        [Required]
        public string h_location { get; set; }
        [Required]
        public string h_loc_url { get; set; }
        [Required]
        public string h_img { get; set; }
        [Required]
        public int h_bloodquantity { get; set; }
    }
}
