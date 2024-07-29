using ProductTask.Entity.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductTask.Entity.Main
{
    [Table("ProductCategories", Schema = "Main")]
    public class ProductCategoryModel : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public CategoryModel Category{ get; set; }
        public Guid ProductId { get; set; }
        public ProductModel Product { get; set; }
    }
}
