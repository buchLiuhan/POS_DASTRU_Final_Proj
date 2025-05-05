using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InitializeProducts();
            Login();
     
        }



        static void Login()
        {

            string Username = "user123", user;
            string Password = "pass123", pass;
            bool tryagain = true;

            while (tryagain)
            {
                string choice;

                Console.Clear();
                // Design for the login screen
                Console.WriteLine("===============================================");
                Console.WriteLine("           Welcome to the POS System           ");
                Console.WriteLine("===============================================");
                Console.WriteLine();

                Console.WriteLine("Please log in to continue.");
                Console.WriteLine();

                Console.Write("Enter Username: ");
                user = Console.ReadLine();
                Console.WriteLine();

                Console.Write("Enter Password: ");
                pass = Console.ReadLine();
                Console.WriteLine();

                if (user == Username && pass == Password)
                {
                    Console.Clear();
                    // Add some design after successful login
                    Console.WriteLine("===============================================");
                    Console.WriteLine("            Login Successful!                 ");
                    Console.WriteLine("===============================================");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadLine();
                    Console.Clear();
                    dashboard();
                    break;
                }
                else
                {
                    Console.Clear();
                    // Design for failed login
                    Console.WriteLine("===============================================");
                    Console.WriteLine("              Login Failed!                   ");
                    Console.WriteLine("===============================================");
                    Console.WriteLine("Please check your credentials and try again.");
                }

                // Ask if the user wants to try again with a friendly prompt
                Console.WriteLine();
                Console.Write("Do you want to try again? [y/n]: ");
                choice = Console.ReadLine();
                Console.WriteLine();

                tryagain = (choice.Equals("y", StringComparison.OrdinalIgnoreCase) ||
                            choice.Equals("yes", StringComparison.OrdinalIgnoreCase));

                if (!tryagain)
                {
                    Console.Clear();
                    // Add a design for exiting
                    Console.WriteLine("===============================================");
                    Console.WriteLine("      Thanks for using the system!            ");
                    Console.WriteLine("===============================================");
                    Console.ReadLine();
                    Environment.Exit(0); // Exit the program
                }
            }


        }



        static void dashboard()
        {

            bool Dashboardrunning = true;
            string choice;

            while (Dashboardrunning)
            {
                Console.Clear();
                // Design for the Dashboard header
                Console.WriteLine("===============================================");
                Console.WriteLine("             Welcome to the Dashboard          ");
                Console.WriteLine("===============================================");
                Console.WriteLine();

                // Options menu with formatting
                Console.WriteLine("Please select an option:");
                Console.WriteLine("1: Make a Sale");
                Console.WriteLine("2: ");
                Console.WriteLine("3: Exit and Logout");
                Console.WriteLine();
                Console.Write("Your choice (1-3): ");
                choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        MakeSale(); // Clear and go to Make Sale
                        break;

                    case "2":
                        Console.Clear();
                        ViewInventory(); // Clear and go to View Inventory
                        break;

                    case "3":
                        Console.Clear();
                        // Exit and logout message
                        Console.WriteLine("===============================================");
                        Console.WriteLine("        Thanks for using the system!           ");
                        Console.WriteLine("===============================================");
                        Console.WriteLine("Press any key to log out...");
                        Console.ReadLine();
                        Console.Clear();
                        Login(); // Call the Login function to go back to login screen
                        Dashboardrunning = false;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("===============================================");
                        Console.WriteLine("              Invalid choice! Please try again ");
                        Console.WriteLine("===============================================");
                        break;
                }
            }



        }//dashboard end


        class Product
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public int Id { get; set; }
            public int Price { get; set; }
            public bool Available { get; set; }

        }

        static List<Product> products = new List<Product>();
        static Stack<string> purchaseHistory = new Stack<string>();

        static void InitializeProducts()
        {
            Random rand = new Random();

            products.Add(new Product { Id = 1, Name = "Ryzen 9 5900X", Price = 550, Type = "CPU" });
            products.Add(new Product { Id = 2, Name = "Ryzen 7 5800X", Price = 400, Type = "CPU" });

            // Motherboards
            products.Add(new Product { Id = 3, Name = "MSI X570 Edge WiFi", Price = 220, Type = "Motherboard" });
            products.Add(new Product { Id = 4, Name = "ASUS B550-F Gaming", Price = 180, Type = "Motherboard" });

            // RAM
            products.Add(new Product { Id = 5, Name = "Corsair LPX 16GB DDR4-3200", Price = 90, Type = "RAM" });
            products.Add(new Product { Id = 6, Name = "G.Skill Ripjaws V 16GB", Price = 100, Type = "RAM" });

            // Storage
            products.Add(new Product { Id = 7, Name = "Samsung 970 Evo 1TB", Price = 140, Type = "Storage" });
            products.Add(new Product { Id = 8, Name = "WD Blue 2TB HDD", Price = 60, Type = "Storage" });

            // GPUs
            products.Add(new Product { Id = 9, Name = "NVIDIA RTX 3080", Price = 700, Type = "GPU" });
            products.Add(new Product { Id = 10, Name = "NVIDIA RTX 3070", Price = 500, Type = "GPU" });



            var shuffle = products.OrderBy(p => rand.Next()).ToList();
            int soldOutCount = rand.Next(3, 5);



            int remaining = soldOutCount;  // copy the count
            foreach (var prod in shuffle)
            {
                if (remaining > 0)
                {
                    prod.Available = false;
                    remaining--;            // decrement the copy
                }
                else
                {
                    prod.Available = true;
                }
            }





        }// initialize method 







        static void MakeSale()
        {
            List<Product> cart = new List<Product>();
            bool saleInProgress = true;

            while (saleInProgress)
            {
                
                Console.WriteLine("Items in your cart:");
                foreach (var item in cart)
                {
                    Console.WriteLine($"{item.Name} - ${item.Price}");
                }

                Console.WriteLine();
                

                Console.WriteLine("Inventory:");
                Console.WriteLine("{0,-4} | {1,-30} | {2,6} | {3,-12} | {4,-12}",
      "ID", "Product Name", "Price", "Type", "Availability");
                Console.WriteLine(new string('-', 4) + "-+-" +
                                  new string('-', 30) + "-+-" +
                                  new string('-', 6) + "-+-" +
                                  new string('-', 12) + "-+-" +
                                  new string('-', 12));

                // Rows
                foreach (var item in products)
                {
                    var availability = item.Available ? "Available" : "Sold Out";
                    Console.WriteLine("{0,-4} | {1,-30} | {2,6:C0} | {3,-12} | {4,-12}",
                        item.Id,
                        item.Name,
                      "$" + item.Price,
                        item.Type,
                        availability);
                }

                Console.WriteLine();
                Console.WriteLine("Options:");
                Console.WriteLine("1: Add item to cart");
                Console.WriteLine("2: Remove item from cart");
                Console.WriteLine("3: Finalize sale");
                Console.WriteLine("4: View last 2 purchase histories");
                Console.WriteLine("5: Return to Dashboard");
                Console.Write("Please select an option (1-5): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter the product ID to add to the cart: ");
                        if (int.TryParse(Console.ReadLine(), out int addId))
                        {
                            var product = products.FirstOrDefault(p => p.Id == addId);
                            if (product != null)
                            {
                                if (product.Available)
                                {
                                    cart.Add(product);
                                    Console.WriteLine($"Added {product.Name} to the cart.");
                                }
                                else
                                {
                                    Console.WriteLine("Item is currently sold out.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid product ID.");
                            }
                        }
                        break;

                    case "2":
                        Console.Write("Enter the product ID to remove from the cart: ");
                        if (int.TryParse(Console.ReadLine(), out int removeId))
                        {
                            var removeProduct = cart.FirstOrDefault(p => p.Id == removeId);
                            if (removeProduct != null)
                            {
                                cart.Remove(removeProduct);
                                Console.WriteLine($"Removed {removeProduct.Name} from the cart.");
                            }
                            else
                            {
                                Console.WriteLine("Product not found in the cart.");
                            }
                        }
                        break;

                    case "3":
                        int total = 0;
                        Console.WriteLine("\nReceipt:");
                        string history = "";
                        foreach (var item in cart)
                        {
                            Console.WriteLine($"{item.Name} - ${item.Price}");
                            total += item.Price;
                            history += $"{item.Name} - ${item.Price}\n";
                        }
                        Console.WriteLine($"Total: ${total}");
                        Console.WriteLine("Sale completed. Thank you!");

                        // Add to purchase history
                        if (purchaseHistory.Count == 2) purchaseHistory.Pop();
                        purchaseHistory.Push(history + $"Total: ${total}\n---");

                        saleInProgress = false;
                        break;

                    case "4":
                        Console.WriteLine("\nLast 2 Purchases:");
                        foreach (var record in purchaseHistory.Reverse())
                        {
                            Console.WriteLine(record);
                        }
                        break;

                    case "5":
                        saleInProgress = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                Console.Clear();


            }// while end





        }// make sale code












        static void ViewInventory() { }






    }
}
