using System.ComponentModel.DataAnnotations;

namespace ProductTaskFrontEnd.Service.Category.Dto;

public class UpdateCategoryDto
{
    public Guid Id { get; set; }
    [Required(ErrorMessage ="هذا الحقل مطلوب")]
    public string Name { get; set; }
}
