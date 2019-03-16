using System.ComponentModel.DataAnnotations;

namespace AKTTool.Models
{
   public class Item : EntityBase
  {
    [Display(Name = "Type")]
    public string type { get; set; }

    [Display(Name = "Code")]
    public string code { get; set; }

    [Display(Name = "Link")]
    public string link { get; set; }
  }
}
