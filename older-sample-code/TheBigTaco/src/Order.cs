using System;
using System.Collections.Generic;

namespace TheBigTaco
{
    class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public List<MenuItem> Items { get; set; } = new List<MenuItem>();
    }
}
