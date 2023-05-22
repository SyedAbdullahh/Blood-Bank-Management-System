using System.ComponentModel.DataAnnotations;
namespace WebApplication4.Models
{
    public class Employee
    {
        [Key]
        public int e_Id { get; set; }
        [Required]
        public string e_Name { get; set; }
        [Required]  
        public string e_Email { get; set; }
        [Required]
        public string e_Password { get; set; }
     
        public string? status { get; set; }
        [Required]  
        public string e_centre_id { get; set;}

    }
}
