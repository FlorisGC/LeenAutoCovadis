using System.ComponentModel.DataAnnotations;

namespace LeenAutoCovadis.Api.Models;

public class Role
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public virtual List<User> Users { get; set; } = new List<User>();

}
