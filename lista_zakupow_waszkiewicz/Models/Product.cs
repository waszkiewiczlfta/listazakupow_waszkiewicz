using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaZakupowWaszkiewicz.Models
{
    public class Product
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public bool IsBought { get; set; }
        public bool IsOptional { get; set; }
        public string CategoryName { get; set; }
        public string Store { get; set; }
    }
}
