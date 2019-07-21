using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace aspMvcCRUD.Models
{
    public class ProductModel
    {

        public int productid { get; set; }

        [DisplayName("Product Name")]
        public String productName { get; set; }
        public decimal price { get; set; }
        public int count { get; set; }
    }
}