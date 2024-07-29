using ProductTask.Entity.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductTask.Entity.Main
{
    [Table("Products", Schema = "Main")]

    public class ProductModel : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price{ get; set; }
        public ICollection<ProductCategoryModel> ProductCategories{ get; set; }
    }
}
