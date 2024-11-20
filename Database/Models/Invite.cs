using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models;

[Table("invites")]
public class Invite
{
    [Required] public string InviteId { get; set; }

    [Required] public string Status { get; set; }


    [Required] public Team Team { get; set; } = null!;

    [Required] public Player Target { get; set; } = null!;

    [Required] public Player Sender { get; set; } = null!;


    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime createdAt { get; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime updatedAt { get; }
}