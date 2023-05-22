using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Transaction
    {
        [Key]
        public int? t_Id { get; set; }
        [Required]
        public string u_name { get; set; }
        [Required]

        public int u_id { get; set; }
        [Required]
        public int h_id { get; set; }
        [Required]
        public int b_id{get;set;}
        [Required]
        public string t_bloodtype { get; set; }
        public string t_status { get; set; }
        [Required]
        public string t_type{ get; set; }
        [Required]
        public int t_bloodquantity { get; set; }
        [Required]
        public int t_bloodprice { get; set; }
        [Required]
        public string t_date { get; set; }
    }
}
