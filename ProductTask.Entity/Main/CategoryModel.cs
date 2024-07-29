using ProductTask.Entity.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductTask.Entity.Main
{
    [Table("Categories", Schema = "Main")]
    public class CategoryModel : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ProductCategoryModel> ProductCategories { get; set; }

    }
}
