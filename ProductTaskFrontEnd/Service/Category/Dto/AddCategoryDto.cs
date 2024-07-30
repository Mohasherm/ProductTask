using System.ComponentModel.DataAnnotations;

namespace ProductTaskFrontEnd.Service.Category.Dto;

public class AddCategoryDto
{
    [Required(ErrorMessage ="هذا الحقل مطلوب")]
    public string Name { get; set; }
}
