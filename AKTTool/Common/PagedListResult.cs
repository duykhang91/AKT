using System.Collections.Generic;

namespace AKTTool.Common
{
  public class PagedListResult<T>
  {
    public List<T> Items { get; set; }

    public int TotalCount { get; set; }

    public int PageCount { get; set; }

    public string CurrentFilter { get; set; }
  }
}
