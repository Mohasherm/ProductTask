using System;
using System.Collections.Generic;

namespace ProductTask.Repository.Main.Product.Dto
{
    public class AddProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public List<Guid> Categories{ get; set; }
    }
}
