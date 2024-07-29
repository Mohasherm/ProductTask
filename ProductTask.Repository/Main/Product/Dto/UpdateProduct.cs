using System.Collections.Generic;
using System;

namespace ProductTask.Repository.Main.Product.Dto
{
    public class UpdateProduct
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public List<Guid> Categories { get; set; }
    }
}
