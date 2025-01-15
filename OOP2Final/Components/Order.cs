namespace ResturantManagmentSystem.Components
{
    internal class Order
    {
        // get or set the orderid for the Order
        public int OrderId { get; set; }
        //get or set the tablenumber
        public int TableNumber { get; set; }
        // Name of the customer
        public string CustomerName { get; set; }
        // List of menu items with the order
        public List<string> Items { get; set; }

        // initializes an order object with provided values
        public Order(int orderId, int tableNumber, string customerName, List<string> items)
        {
            // Assigning values
            OrderId = orderId;
            TableNumber = tableNumber;
            CustomerName = customerName;
            Items = items;
        }

        // Default constructor
        public Order()
        {
            Items = new List<string>();
        }
    }
}
