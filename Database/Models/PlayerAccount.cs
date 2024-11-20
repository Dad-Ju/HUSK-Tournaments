using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models;

[Table("player_accounts")]
public class PlayerAccount
{
    [Required] public Player Player { get; set; } = null!;

    [Required] public string type { get; set; }

    [Required] public string identifier { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime createdAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime updatedAt { get; set; }
}