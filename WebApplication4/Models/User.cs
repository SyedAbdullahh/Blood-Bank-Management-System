using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class User
    {
        [Key]
        public int? u_Id { get; set; }
        [Required]
        public string u_Name { get; set; }
        [Required]
        public string u_Email { get; set; }
        [Required]
        public string u_Password { get; set;}
        [Required]
        public string u_PhoneNumber { get; set;}
        [Required]
        public  string u_Address { get; set; }
        [Required]
        public  string u_bloodgroup { get; set; }
     
        public int? u_wallet{ get; set; }

        public string? u_status { get; set; }
   

    }
    
}
