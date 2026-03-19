using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tmp;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    public Notebook? Notebook { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
