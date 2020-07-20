using System.ComponentModel.DataAnnotations;

namespace TenmoServer.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public decimal Balance { get; set; }
    }
}