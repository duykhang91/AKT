using System;
using System.Collections.Generic;
using System.Text;

namespace AKTTool.Models
{
  public interface IAuditable
  {
    string CreatedBy { get; set; }

    DateTime CreatedDateUtc { get; set; }

    string ModifiedBy { get; set; }

    DateTime? ModifiedDateUtc { get; set; }
  }
}
