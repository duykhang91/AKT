using System.Collections.Generic;

namespace AKTTool.Models
{
  public class Door : EntityBase
  {
    public Item input { get; set; }
    public List<Item> doors { get; set; }
  }
}
