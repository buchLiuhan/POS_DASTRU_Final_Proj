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

        /* xsssssss
         
        To do:
                 -Replace the price from dollar to peso sign -- done
                 -Utilize arrays 2d and 1d -- done
                 - Checkout method for the reciept -- done.
                 - Quantity of products like if the customer buys the same product it wont look like this: -- done

                       product a
                       product a
                   
                    But should be like this: product a x2 or product a = 2 -- done done done


                 -prize calculates as soon as the product is added -- done
                 -Cashier schedule like time in and time out there should be a timer that counts the amount of time they have worked would replace option 2 on dashboard needs more thought -- in progress
                 -Multiple accounts - done
                 -Password Concealment - done
                 -Bug checking should be done as soon as the build is done - after everything
                 -Utilize Stack  for history - After cashier schedule
                
                 
         
         
         
         
         */


        static string ReadPassword()
        {
            var pwd = new System.Text.StringBuilder();
            ConsoleKeyInfo key;
            while ((key = Console.ReadKey(intercept: true)).Key != ConsoleKey.Enter)
            {
                if (key.Key == ConsoleKey.Backspace)
                {
                    if (pwd.Length > 0)
                    {
                        pwd.Length--;
                        Console.Write("\b \b"); // erase last '*'
                    }
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    pwd.Append(key.KeyChar);
                    Console.Write('*');
                }
            }
            Console.WriteLine();
            return pwd.ToString();
        }






        static void Login()
        {

            string[] users = { "Mathilda", "Leslie", "Rafaela", "1"};
            string[] passwords = { "roamer", "marksmen", "roamer1", "1" };

            string  user;
            string pass;
            bool tryAgain = true;

            while (tryAgain)
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
                pass = ReadPassword();
                Console.WriteLine();


                int indexValidaccount = -1;
                for (int i = 0; i < users.Length; i++)
                {

                    if (users[i] == user && passwords[i] == pass)
                    {

                        indexValidaccount = i;
                        break;

                    }

                }

              if(indexValidaccount >= 0) 
               {

                    Console.Clear();
                    // Design for successful login
                    Console.WriteLine("===============================================");
                    Console.WriteLine("            Login Successful!                  ");
                    Console.WriteLine("===============================================");
                    Console.WriteLine($"         Welcome, {users[indexValidaccount]}!             ");
                    Console.WriteLine("===============================================");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    dashboard();
                    return;

                }

              else
              {

                    Console.Clear();
                    // Design for failed login
                    Console.WriteLine("===============================================");
                    Console.WriteLine("              Login Failed!                    ");
                    Console.WriteLine("===============================================");
                    Console.WriteLine("Invalid username or password.");

                }




                // Ask if the user wants to try again 
                Console.WriteLine();
                Console.Write("Do you want to try again? [y/n]: ");
                choice = Console.ReadLine();
                Console.WriteLine();

                tryAgain = (choice.Equals("y", StringComparison.OrdinalIgnoreCase) ||
                            choice.Equals("yes", StringComparison.OrdinalIgnoreCase));

                if (!tryAgain)
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



        static void dashboard()//might be removed entirely
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
                Console.WriteLine("1: Make a sale");
                Console.WriteLine("2: Check schedule");
                Console.WriteLine("3: Exit and logout ");// this block has become redundant/ unecessary so must be replace or replaced

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



        }//program class end

        //Expiremental dont touch will be remove if redundant
        class Product
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public int Id { get; set; }
            public int Price { get; set; }

        }

        static List<Product> products = new List<Product>();
        static Stack<string> purchaseHistory = new Stack<string>();

        static void InitializeProducts()
        {
            int exchangeratepeso = 56;
            Random rand = new Random();

            products.Add(new Product { Id = 1, Name = "Ryzen 9 5900X", Price = 550 * exchangeratepeso, Type = "CPU" });
            products.Add(new Product { Id = 2, Name = "Ryzen 7 5800X", Price = 400 * exchangeratepeso, Type = "CPU" });

            // Motherboards
            products.Add(new Product { Id = 3, Name = "MSI X570 Edge WiFi", Price = 220 * exchangeratepeso, Type = "Motherboard" });
            products.Add(new Product { Id = 4, Name = "ASUS B550-F Gaming", Price = 180 * exchangeratepeso, Type = "Motherboard" });

            // RAM
            products.Add(new Product { Id = 5, Name = "Corsair LPX 16GB DDR4", Price = 90 * exchangeratepeso, Type = "RAM" });
            products.Add(new Product { Id = 6, Name = "G.Skill Ripjaws V 16GB", Price = 100 * exchangeratepeso, Type = "RAM" });

            // Storage
            products.Add(new Product { Id = 7, Name = "Samsung 970 Evo 1TB", Price = 140 * exchangeratepeso, Type = "Storage" });
            products.Add(new Product { Id = 8, Name = "WD Blue 2TB HDD", Price = 60 * exchangeratepeso, Type = "Storage" });

            // GPUs
            products.Add(new Product { Id = 9, Name = "NVIDIA RTX 3080", Price = 700 * exchangeratepeso, Type = "GPU" });
            products.Add(new Product { Id = 10, Name = "NVIDIA RTX 3070", Price = 500 * exchangeratepeso, Type = "GPU" });


        }//end of expiremental












        //part of the main function
        static void MakeSale()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;//for peso sign so it doesn't display '?'

            List<Product> cart = new List<Product>();
            bool saleInProgress = true;

            while (saleInProgress)
            {
                int total = 0;
                Console.WriteLine("Items in your cart:");

                var groupedCart = cart.GroupBy(item => item.Id);// a function for displaying items like this: 2x item.

                foreach (var group in groupedCart)
                {
                    var product = group.First(); // Get the first item from the grouped items (since they are identical)
                    int quantity = group.Count();   // Count the number of items in this group
                    int totalPrice = product.Price * quantity;  // Calculate the total price for the grouped items
                    if (quantity > 1) Console.WriteLine($"{quantity}x {product.Name} - ₱{totalPrice:F2}"); // If the quantity is greater than one, show it as '2x' along with the total price
                    else Console.WriteLine($"{quantity}x {product.Name} - ₱{product.Price:F2}");// If there's only one item, just print the product name and price
                    total = total += totalPrice;


                }

                Console.WriteLine($"--------------------------------|Total: {total:F2}|");
                Console.WriteLine();


                Console.WriteLine("Inventory:");
                Console.WriteLine("{0,-4} | {1,-25} | {2,16} | {3,-12}", "ID", "Product Name", "Price", "Type");
                Console.WriteLine(new string('-', 4) + "-+-" + new string('-', 25) + "-+-" + new string('-', 16) + "-+-" + new string('-', 12));

                // Rows
                foreach (var item in products)
                {

                    Console.WriteLine("{0,-4} | {1,-25} | ₱{2,15:F2} | {3,-12}", item.Id, item.Name, item.Price, item.Type);
                }

                Console.WriteLine();
                Console.WriteLine("Options:");
                Console.WriteLine("1: Add item to cart");
                Console.WriteLine("2: Remove item from cart");
                Console.WriteLine("3: Finalize sale");
                Console.WriteLine("4: Return to Dashboard");
                Console.Write("Please select an option (1-4): ");
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
                                cart.Add(product);
                                Console.WriteLine($"Added {product.Name} to the cart.");

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

                    case "3"://finish today almost done

                        Payment(cart, total);
                        saleInProgress = true;
                        break;

                    case "4":
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












        static void Payment(List<Product> cart, int total)//priorotize this
        {

            Console.Clear();
            int subtotal = total;
            double vatRate = 0.12;
            double vat = subtotal * vatRate;
            double finalTotal = subtotal + vat;

            Console.WriteLine("---------- RECEIPT ----------");
            Console.WriteLine("Store Name:   L's Tech Shop");
            Console.WriteLine($"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine("----------------------------");

            foreach (var group in cart.GroupBy(item => item.Id))
            {
                var product = group.First();
                int quantity = group.Count();
                int totalPrice = product.Price * quantity;
                Console.WriteLine($"{quantity,2}x {product.Name,-20} - ₱{totalPrice,8:F2}");
            }

            Console.WriteLine("----------------------------");
            Console.WriteLine($"Subtotal:        ₱{subtotal,8:F2}");
            Console.WriteLine($"VAT (12%):      ₱{vat,8:F2}");
            Console.WriteLine($"Total:          ₱{finalTotal,8:F2}");
            Console.WriteLine("----------------------------");

            Console.WriteLine("Enter '00' to exit");
            Console.Write($"Enter payment amount:  ₱");
            string input = Console.ReadLine();

            if (input == "00")
            {
                Console.WriteLine("Cancelling payment...");
                return;
            }






            if (double.TryParse(input, out double payment))
            {
                double change = payment - finalTotal;
                if (change >= 0)
                {
                    Console.WriteLine($"Change:  ₱{change,8:F2}");
                    Console.WriteLine("===================================================+");
                    Console.WriteLine("Payment successful. Please press Enter to print.");
                    Console.WriteLine("===================================================+");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Insufficient payment. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid payment.");
            }
            



            /* Console.WriteLine("\nLast 2 Purchases:");
            foreach (var record in purchaseHistory.Reverse())
            {
                Console.WriteLine(record);
            } */ // for use later // utilizes the stack will be used for purchase history also we should add exit to this method for user friendliness






        }












    }
}
