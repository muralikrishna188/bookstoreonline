using System;
using System.Collections.Generic;

namespace AzureCourse.Function
{
    public class Order
    {
        public int orderId { get; set; }
        public DateTime orderDate { get; set; }
        public double total { get; set; }
        public int priority  {get; set;}
        public List<OrderItem> items { get; set; }

        public Order()  {
            var rand=new Random();
            orderId=rand.Next(1000);
            priority=rand.Next(3)+1;
        }
    }

    public class OrderItem
    {
        public String name { get; set; }
        public int id { get; set; }
        public double price { get; set; }
    }
}