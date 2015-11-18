using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderEntry.CodeCofe.Web.Models
{
    public class OrderModels
    {
        public int ID { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public string DeliveryAddress { get; set; }    
        public DateTime OrderDate { get; set; }
    }
}