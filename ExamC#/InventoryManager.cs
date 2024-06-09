using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamC_
{
    public class InventoryManager
    {
        private string Path = @"C:\Users\User\source\repos\ExamC#\ExamC#\bin\Debug\net7.0\inventory.txt";

        private List<Item> inventory = new List<Item>();
        public void AddItem(Item newItem)
        {
            inventory.Add(newItem);
        }

        public void SaveInventory(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (var item in inventory)
                {
                    writer.WriteLine($"{item.Name},{item.Quantity},{item.Price},{item.Category},{item.Type}");
                }
            }
        }


        public void LoadInventory(string filename)
        {
            if (File.Exists(filename))
            {
                inventory.Clear();
                using (StreamReader reader = new StreamReader(filename))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 5)
                        {
                            Item item = new Item
                            {
                                Name = parts[0],
                                Quantity = int.Parse(parts[1]),
                                Price = double.Parse(parts[2]),
                                Category = parts[3],
                                Type = parts[4]
                            };
                            inventory.Add(item);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Inventory file not found.");
            }
        }


        public void DisplayInventory()
        {
            Console.WriteLine("Inventory:");
            foreach (var item in inventory)
            {
                Console.WriteLine($"Name: {item.Name}, Quantity: {item.Quantity}, Price: ${item.Price}, Category: {item.Category}, Type: {item.Type}");
            }
        }


        public void SearchItemByName(string name)
        {
            var foundItems = inventory.FindAll(item => item.Name.ToLower().Contains(name.ToLower()));
            if (foundItems.Count > 0)
            {
                Console.WriteLine($"Found items with name '{name}':");
                foreach (var item in foundItems)
                {
                    Console.WriteLine($"Name: {item.Name}, Quantity: {item.Quantity}, Price: ${item.Price}, Category: {item.Category}");
                }
            }
            else
            {
                Console.WriteLine($"Item with name '{name}' not found.");
            }
        }

        public void SearchItemByCategory(string category)
        {
            var foundItems = inventory.FindAll(item => item.Category.ToLower() == category.ToLower());
            PrintSearchResults(foundItems, category);
        }

        public void SearchItemByPrice(double minPrice)
        {
            var foundItems = inventory.FindAll(item => item.Price > minPrice);
            PrintSearchResults(foundItems, $"Price > ${minPrice}");
        }


        public void SearchItemByType(string type)
        {
            var foundItems = inventory.FindAll(item => item.Type.ToLower() == type.ToLower());
            PrintSearchResults(foundItems, type);
        }

        private void PrintSearchResults(List<Item> items, string searchCriteria)
        {
            if (items.Count > 0)
            {
                Console.WriteLine($"Found items matching '{searchCriteria}':");
                foreach (var item in items)
                {
                    Console.WriteLine($"Name: {item.Name}, Quantity: {item.Quantity}, Price: ${item.Price}, Category: {item.Category}, Type: {item.Type}");
                }
            }
            else
            {
                Console.WriteLine($"No items found matching '{searchCriteria}'.");
            }
        }

        public bool RemoveItem(string itemName)
        {
            Item itemToRemove = inventory.FirstOrDefault(item => item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            if (itemToRemove != null)
            {
                inventory.Remove(itemToRemove);
                return true;
            }
            return false;
        }

        public void MoveFile()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter the path of the file to move: ");
            string sourcePath = Console.ReadLine();

            
            string destinationPath = Path;

            try
            {
                File.Move(sourcePath, destinationPath);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("File moved successfully.");
                Path = sourcePath;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.ResetColor();
        }
    }
}
