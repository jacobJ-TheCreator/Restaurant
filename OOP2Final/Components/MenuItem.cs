using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagmentSystem.Components
{
    internal class MenuItem
    {
        // Property to store the ID of the menu item
        public int ItemId { get; set; }

        // Property to store the name of the menu item
        public string Name { get; set; }

        // Property to store the cost of the menu item
        public double Cost { get; set; }

        // Property to store the description of the menu item
        public string Description { get; set; }

        // Property to track whether the menu item is being edited
        public bool IsEditing { get; set; }

        // Constructor to initialize a menuitem object with specific values, setting isediting to false by default
        public MenuItem(int itemId, string name, double cost, string description)
        {
            ItemId = itemId;
            Name = name;
            Cost = cost;
            Description = description;
            IsEditing = false;
        }

        // initializes a menuitem object with default values, setting isediting to false
        public MenuItem()
        {
            IsEditing = false;
        }
    }
}
