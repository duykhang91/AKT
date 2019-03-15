using System.Collections.Generic;

namespace AKTTool.Models
{
  public class Door : EntityBase
  {
    public Common input { get; set; }
    public List<Common> doors { get; set; }
  }
}
