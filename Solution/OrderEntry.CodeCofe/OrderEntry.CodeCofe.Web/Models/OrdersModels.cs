using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderEntry.CodeCofe.Web.Models
{
    public class OrdersModels
    {
        public int ID { get; set; }
        public DateTime  OrderDate{ get; set; }
        public string OrderNumber{ get; set; }
        public string CustomerName { get; set; }
        public string  DeliveryAddress { get; set; }
        public decimal  QuantityOrdered { get; set; }
    }
}