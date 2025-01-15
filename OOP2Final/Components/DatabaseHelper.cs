//Authors:
//Mostapha
//Jacob
//Noah
//Anton
//Version: 2024-08-12
//Description: This class manages the SQLite database for the Restaurant Management System, including the creation and maintenance of tables.
//It supports various operations for users, menu items, and orders, such as create, read, updare, and delete.
//Additionally, it provides functionality to retrieve and delete order history.
//This ensures the system maintains accurate and up to date records.



using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using Microsoft.Data.Sqlite;
using ResturantManagmentSystem.Components;

namespace ResturantManagmentSystem.Components
{
    internal class DatabaseHelper
    {
        private string databaseName;

        public bool IsInfoCovered { get; set; }

        // Initializes the DatabaseHelper with a DB name
        public DatabaseHelper(string dbName)
        {
            databaseName = dbName;
            CreateDatabase(); // Ensure DB and tables are created when the helper is instantiated
        }

        // Method to create the DB and tables if they do not exist
        public void CreateDatabase()
        {
            string connectionString = $"Data Source={databaseName}.db";
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        Users_id INTEGER PRIMARY KEY AUTOINCREMENT,
                        first_name TEXT NOT NULL,
                        last_name TEXT NOT NULL,
                        Users_email TEXT NOT NULL,
                        Users_phone TEXT NOT NULL,
                        position TEXT NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS MenuItem (
                        Item_id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Item_name TEXT NOT NULL,
                        Item_cost REAL NOT NULL,
                        Item_description TEXT NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS Orders (
                        Order_id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Table_number INTEGER NOT NULL,
                        Customer_name TEXT NOT NULL,
                        Menu_items TEXT NOT NULL
                    );
                ";
                command.ExecuteNonQuery();
            }
        }

        // Method to delete the DB file
        public void DeleteDatabase()
        {
            File.Delete($"{databaseName}.db");
        }


        // Method to retrieve all users from the users table
        public List<User> GetUsers()
        {
            string connectionString = $"Data Source={databaseName}.db";
            var users = new List<User>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Users";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User
                        {
                            UserId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Email = reader.GetString(3),
                            Phone = reader.GetString(4),
                            Position = reader.GetString(5)
                        };
                        users.Add(user);
                    }
                }
            }

            return users;
        }

        // Method to add a new user to the Users table

        public void AddUser(string firstName, string lastName, string email, string phone, string position)
        {
            string connectionString = $"Data Source={databaseName}.db";
            IsInfoCovered = false;

            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        INSERT INTO Users (first_name, last_name, Users_email, Users_phone, position)
                        VALUES ($firstName, $lastName, $email, $phone, $position)
                    ";
                    command.Parameters.AddWithValue("$firstName", firstName);
                    command.Parameters.AddWithValue("$lastName", lastName);
                    command.Parameters.AddWithValue("$email", email);
                    command.Parameters.AddWithValue("$phone", phone);
                    command.Parameters.AddWithValue("$position", position);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                IsInfoCovered = true;
            }
        }

        // Method to update an existing user in the users table

        public void UpdateUser(int id, string firstName, string lastName, string email, string phone, string position)
        {
            string connectionString = $"Data Source={databaseName}.db";

            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        UPDATE Users 
                        SET first_name = $firstName, last_name = $lastName, Users_email = $email, Users_phone = $phone, position = $position 
                        WHERE Users_id = $id
                    ";
                    command.Parameters.AddWithValue("$id", id);
                    command.Parameters.AddWithValue("$firstName", firstName);
                    command.Parameters.AddWithValue("$lastName", lastName);
                    command.Parameters.AddWithValue("$email", email);
                    command.Parameters.AddWithValue("$phone", phone);
                    command.Parameters.AddWithValue("$position", position);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                IsInfoCovered = true;
            }
        }

        // Method to delete a user from the users table
        public void DeleteUser(int id)
        {
            string connectionString = $"Data Source={databaseName}.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    DELETE FROM Users 
                    WHERE Users_id = $id
                ";
                command.Parameters.AddWithValue("$id", id);

                command.ExecuteNonQuery();
            }
        }

        // Method to retrieve all menu items from the menuitem table
        public List<MenuItem> GetMenuItems()
        {
            string connectionString = $"Data Source={databaseName}.db";
            var menuItems = new List<MenuItem>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM MenuItem";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new MenuItem
                        {
                            ItemId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Cost = reader.GetDouble(2),
                            Description = reader.GetString(3)
                        };
                        menuItems.Add(item);
                    }
                }
            }

            return menuItems;
        }

        // Method to add a new menu item to the menuitem table
        public void AddMenuItem(string name, double cost, string description)
        {
            string connectionString = $"Data Source={databaseName}.db";
            IsInfoCovered = false;

            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        INSERT INTO MenuItem (Item_name, Item_cost, Item_description)
                        VALUES ($name, $cost, $description)
                    ";
                    command.Parameters.AddWithValue("$name", name);
                    command.Parameters.AddWithValue("$cost", cost);
                    command.Parameters.AddWithValue("$description", description);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                IsInfoCovered = true;
            }
        }

        // Method to update an existing menu item in the menuitem table
        public void UpdateMenuItem(int id, string name, double cost, string description)
        {
            string connectionString = $"Data Source={databaseName}.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE MenuItem 
                    SET Item_name = $name, Item_cost = $cost, Item_description = $description 
                    WHERE Item_id = $id
                ";
                command.Parameters.AddWithValue("$id", id);
                command.Parameters.AddWithValue("$name", name);
                command.Parameters.AddWithValue("$cost", cost);
                command.Parameters.AddWithValue("$description", description);

                command.ExecuteNonQuery();
            }
        }

        // Method to delete a menu item from the menuitem table
        public void DeleteMenuItem(int id)
        {
            string connectionString = $"Data Source={databaseName}.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    DELETE FROM MenuItem 
                    WHERE Item_id = $id
                ";
                command.Parameters.AddWithValue("$id", id);

                command.ExecuteNonQuery();
            }
        }

        // Method to retrieve all orders from the orders table
        public List<Order> GetOrders()
        {
            string connectionString = $"Data Source={databaseName}.db";
            var orders = new List<Order>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT Order_id, Table_number, Customer_name, Menu_items FROM Orders";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int orderId = reader.GetInt32(0);
                        int tableNumber = reader.GetInt32(1);
                        string customerName = reader.GetString(2);
                        string menuItems = reader.GetString(3);

                        var items = new List<string>(menuItems.Split(','));

                        var order = new Order(orderId, tableNumber, customerName, items);
                        orders.Add(order);
                    }
                }
            }

            return orders;
        }

        // Method to add a new order to the orders table
        public void AddOrder(int tableNumber, string customerName, List<string> items)
        {
            string connectionString = $"Data Source={databaseName}.db";
            IsInfoCovered = false;

            if (string.IsNullOrEmpty(customerName))
            {
                IsInfoCovered = true;
                return;
            }

            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    foreach (var item in items)
                    {
                        var command = connection.CreateCommand();
                        command.CommandText = @"
                            INSERT INTO Orders (Table_number, Customer_name, Menu_items)
                            VALUES ($tableNumber, $customerName, $item)
                        ";
                        command.Parameters.AddWithValue("$tableNumber", tableNumber);
                        command.Parameters.AddWithValue("$customerName", customerName);
                        command.Parameters.AddWithValue("$item", item);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                IsInfoCovered = true;
            }
        }

        // Method to delete an order from the orders table using the table number
        public void DeleteOrder(int tableNumber)
        {
            string connectionString = $"Data Source={databaseName}.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    DELETE FROM Orders 
                    WHERE Table_number = $tableNumber
                ";
                command.Parameters.AddWithValue("$tableNumber", tableNumber);

                command.ExecuteNonQuery();
            }
        }

        // Method to retrieve the order history from the orders table
        public List<OrderHistoryItem> GetOrderHistory()
        {
            var orderHistory = new List<OrderHistoryItem>();
            string connectionString = $"Data Source={databaseName}.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT Order_id, Table_number, Customer_name, Menu_items FROM Orders";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var order = new OrderHistoryItem
                        {
                            OrderId = reader.GetInt32(0),
                            TableNumber = reader.GetInt32(1),
                            CustomerName = reader.GetString(2),
                            Items = reader.GetString(3).Split(',').ToList()
                        };
                        orderHistory.Add(order);
                    }
                }
            }
            return orderHistory;
        }

        // Method to delete a specific order from the order history in the orders table
        public void DeleteOrderHistory(int orderId)
        {
            string connectionString = $"Data Source={databaseName}.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    DELETE FROM Orders 
                    WHERE Order_id = $orderId
                ";
                command.Parameters.AddWithValue("$orderId", orderId);

                command.ExecuteNonQuery();
            }
        }
    }
}

