using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW0505
{
    class OrderCustomer
    {
        public string name { get; set; }
        public int id { get; set; }
        public int orderId { get; set; }
        public int price { get; set; }
        public int date { get; set; }        

        public OrderCustomer(string name, int id, int orderId, int price, int date)
        {
            this.name = name;
            this.id = id;
            this.orderId = orderId;
            this.price = price;
            this.date = date;
        }
    }

}
