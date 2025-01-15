using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagmentSystem.Components
{
    internal class OrderHistoryItem
    {
        //get or set the orderid for the item 
        public int OrderId { get; set; }
        //get or set the tablenuber 
        public int TableNumber { get; set; }
        // get or set the name of the customer
        public string CustomerName { get; set; }
        // List of menu items with the order
        public List<string> Items { get; set; }

        // initializes an OrderHistoryItem object with the below values
        public OrderHistoryItem(int orderId, int tableNumber, string customerName, List<string> items)
        {
            // Assigning values
            OrderId = orderId;
            TableNumber = tableNumber;
            CustomerName = customerName;
            Items = items;
        }

        // Default constructor
        public OrderHistoryItem()
        {
            Items = new List<string>();
        }
    }
}
