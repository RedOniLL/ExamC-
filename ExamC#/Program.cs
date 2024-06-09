namespace ExamC_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InventoryManager manager = new InventoryManager();
            string filename = "inventory.txt";
            manager.LoadInventory(filename);


            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to Inventory Management System");
            Console.WriteLine("--------------------------------------");


            while (true)
            {
                

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nSelect an option:");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1. Add Item");
                Console.WriteLine("2. Display Inventory");
                Console.WriteLine("3. Search Item by Name");
                Console.WriteLine("4. Search Item by Category");
                Console.WriteLine("5. Search Item by Price");
                Console.WriteLine("6. Search Item by Type");
                Console.WriteLine("7. Remove Item");
                Console.WriteLine("8. Change file directory");
                Console.WriteLine("0. Exit");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Enter your choice: ");
               
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddItem(manager);
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        manager.DisplayInventory();
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        SearchItemByName(manager);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        SearchItemByCategory(manager);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case 5:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        SearchItemByPrice(manager);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case 6:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        SearchItemByType(manager);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case 7:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        RemoveItem(manager);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case 8:
                        MoveFile(manager);
                        break;
                    case 0:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        manager.SaveInventory(filename);
                        Console.WriteLine("Inventory saved to file. Exiting...");
                        Console.ForegroundColor = ConsoleColor.White;
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }

                Console.WriteLine("continue? - press any key");
                Console.ReadLine();
                Console.Clear();

            }
        }

        
        static void AddItem(InventoryManager manager)
        {
            Console.WriteLine("\nAdding Item");
            Console.Write("Enter item name: ");
            string name = Console.ReadLine();

            Console.Write("Enter item quantity: ");
            int quantity;
            while (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity. Please enter a positive integer.");
                Console.Write("Enter item quantity: ");
            }

            Console.Write("Enter item price: ");
            double price;
            double.TryParse(Console.ReadLine(), out price);
            

            Console.Write("Enter item category: ");
            string category = Console.ReadLine();

            Console.Write("Enter item type: ");
            string type = Console.ReadLine();

            manager.AddItem(new Item { Name = name, Quantity = quantity, Price = price, Category = category, Type = type });
            Console.WriteLine("Item added successfully.");
        }

        static void SearchItemByName(InventoryManager manager)
        {
            Console.Write("\nEnter item name to search: ");
            string name = Console.ReadLine();
            manager.SearchItemByName(name);
        }

        static void SearchItemByCategory(InventoryManager manager)
        {
            Console.Write("\nEnter item category to search: ");
            string category = Console.ReadLine();
            manager.SearchItemByCategory(category);
        }

        static void SearchItemByPrice(InventoryManager manager)
        {
            Console.Write("\nEnter minimum price to search: ");
            double minPrice;
            while (!double.TryParse(Console.ReadLine(), out minPrice) || minPrice < 0)
            {
                Console.WriteLine("Invalid price. Please enter a non-negative number.");
                Console.Write("Enter minimum price to search: ");
            }
            manager.SearchItemByPrice(minPrice);
        }

        static void SearchItemByType(InventoryManager manager)
        {
            Console.Write("\nEnter item type to search: ");
            string type = Console.ReadLine();
            manager.SearchItemByType(type);
        }

        static void RemoveItem(InventoryManager manager)
        {
            Console.Write("\nEnter the name of the item to remove: ");
            string name = Console.ReadLine();
            bool removed = manager.RemoveItem(name);
            if (removed)
            {
                Console.WriteLine($"Item '{name}' removed successfully.");
            }
            else
            {
                Console.WriteLine($"Item '{name}' not found.");
            }
        }

        static void MoveFile(InventoryManager manager)
        {
            manager.MoveFile();
        }
    }
    
}
