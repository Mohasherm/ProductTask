using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace ProductTaskFrontEnd.Service.Prodeuct.Dto;

public class AddProduct
{
    [Required(ErrorMessage = "هذا الحقل مطلوب")]
    public string Name { get; set; }
    [Required(ErrorMessage = "هذا الحقل مطلوب")]
    public string Description { get; set; }
    [Required(ErrorMessage = "هذا الحقل مطلوب")]
    [Min(1)]
    public float Price { get; set; }
    [Required(ErrorMessage = "هذا الحقل مطلوب")]
    public List<Guid> Categories { get; set; }
}
