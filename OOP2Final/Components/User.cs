using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagmentSystem.Components
{
    internal class User
    {
        // Get or set the ID for the user
        public int UserId { get; set; }

        // Get or set the first name for the user
        public string FirstName { get; set; }

        // Get or set the last name for the user
        public string LastName { get; set; }

        // Get or set the email address for the user
        public string Email { get; set; }

        // Get or set the phone number for the user
        public string Phone { get; set; }

        // Get or set the position or role for the user
        public string Position { get; set; }

        // Get or set whether the user is being edited
        public bool IsEditing { get; set; }

        //initializes a user object with specific values, setting isediting to false by default
        public User(int userId, string firstName, string lastName, string email, string phone, string position)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Position = position;
            IsEditing = false;
        }

        // initializes a user object with default values, setting isediting to false
        public User()
        {
            IsEditing = false;
        }
    }
}
