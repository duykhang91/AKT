using AKTTool.Common;
using System.ComponentModel.DataAnnotations;

namespace AKTTool.Models
{
  public class GeneralView : EntityBase
  {
    public Item item { get; set; }

    public PagedListResult<Item> generals { get; set; }
  }

  public class Item : EntityBase
  {
    [Display(Name = "Type")]
    [Required]
    public string type { get; set; }

    [Display(Name = "Code")]
    [Required]
    public string code { get; set; }

    [Display(Name = "Link")]
    [Required]
    [Url]
    public string link { get; set; }
  }
}
