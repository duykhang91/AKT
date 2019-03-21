using System.ComponentModel.DataAnnotations;

namespace AKTTool.Models
{
  public abstract class EntityBase
  {
    [Key]
    public int id { get; set; }
  }
}
