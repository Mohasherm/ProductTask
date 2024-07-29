using System;

namespace ProductTask.Repository.Main.Product.Dto
{
    public class GetProductsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
    }
}
