using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace tmp;

public class Notebook
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int UserId { get; set; }

    [JsonIgnore]
    public User User { get; set; } = null!;

    public string? Notes { get; set; }
}
