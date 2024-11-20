using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models;

[Table("players")]
public class Player
{
    [Key] [Required] public string PlayerID { get; set; }

    public string discordId { get; set; }

    [Required] public string Description { get; set; }

    public Team? Team { get; set; } = null!;

    public ICollection<Invite>? Invites { get; } = new List<Invite>();
    public ICollection<PlayerAccount>? PlayerAccounts { get; } = new List<PlayerAccount>();


    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime createdAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime updatedAt { get; set; }
}