using System.ComponentModel.DataAnnotations;

namespace AKTTool.Models
{
  public abstract class EntityBase
  {
    [Key]
    public int Id { get; set; }
    public byte[] RowVersion { get; set; }
  }
}
