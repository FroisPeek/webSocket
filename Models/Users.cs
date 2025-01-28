using System.ComponentModel.DataAnnotations;

namespace wsapi.Models;

public class Users
{
    [Key]
    public int codigo { get; set; }
    public string email { get; set; }
    public string nome { get; set; }
    public string senha { get; set; }
}