using System.ComponentModel.DataAnnotations;
using System.Data;

namespace LeenAutoCovadis.Api.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }


        public virtual List<Role> Roles { get; set; } = new List<Role>();

    }
}