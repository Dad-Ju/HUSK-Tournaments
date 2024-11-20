using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models;

[Table("teams")]
public class Team
{
    [Required] public string TeamId { get; set; }

    public string TeamName { get; set; }
    public string TeamDescription { get; set; }
    public string TeamLogo { get; set; }

    public ICollection<Player> Players { get; set; } = new List<Player>();
    public ICollection<string> Matches { get; set; } = new List<string>();
    public ICollection<Invite> Invites { get; set; } = new List<Invite>();


    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime createdAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime updatedAt { get; set; }
}