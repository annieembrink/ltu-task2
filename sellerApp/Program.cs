using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

//This app is far from perfect and I've tried to comment things that I know could...
//...be better but that I just feel stuck with...
//Honestly I don't think I will come much further without some feedback...
//Everything should work like demanded though

namespace sellerApp
{
    // I spent a lot of time working with the code below (public class seller and public seller)
    //...and I can't say that I fully understand it but I guess it is a process...
    public class seller
    {
        public string name;
        public string id;
        public string district;
        public int products;

        public seller(string name, string id, string district, int products)
        {
            this.name = name;
            this.id = id;
            this.district = district;
            this.products = products;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Path to the textfile
            string filePath = @"C:\Users\anisan-1\OneDrive - ltu.se\inlämning2\ConsoleApp inlämning 2\sellerApp\sellerApp\sellerApp\TextFile1.txt";
            
            //Make new textfile for every time you run the program, so it's empty to start with
            File.Delete(filePath);

            // I suspect that the code would look better if it was broken down into functions
            //...and that everything wasn't inside static void Main (?)
            //...but I'm still learning and figuring that out
            Console.WriteLine("Welcome to the app!");

            Console.Write("How many sellers do you want to registrate? ");
            int count = int.Parse(Console.ReadLine());

            //Creating my list, for my objects
            List<seller> sellers = new List<seller>();

            for (int i = 0; i < count; i++)
            {
                //Collecting the input from the user
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("ID: ");
                string id = Console.ReadLine();
                Console.Write("District: ");
                string district = Console.ReadLine();
                Console.Write("Products: ");
                int products = int.Parse(Console.ReadLine());

                //Creating the object
                seller myObj = new seller(name, id, district, products);

                //Adding object to my list
                sellers.Add(myObj);
            }

            //Sorting sellers after how many products they have sold
            sellers.Sort(delegate (seller x, seller y)
            {
                return x.products.CompareTo(y.products);
            });

            //This list is for items that fullfil a certain condition
            //...like (item.products < 50), to count how many items that fullfill the condition
            //..to begin with I'll create one list for every condition, but it's not a great solution
            List<seller> sellersCount = new List<seller>();
            List<seller> sellersCount1 = new List<seller>();
            List<seller> sellersCount2 = new List<seller>();
            List<seller> sellersCount3 = new List<seller>();

            foreach (var item in sellers)
            {
                if (item.products < 50)
                {
                    //For each time an item fullfil the condition above, add it to the list
                    sellersCount.Add(item);
                }
                else if (item.products > 49 && item.products < 100)
                {
                    sellersCount1.Add(item);
                }
                else if (item.products > 99 && item.products < 200)
                {
                    sellersCount2.Add(item);
                } else
                {
                    sellersCount3.Add(item);
                }
            }
            //Count the items in the list
            //Again, doesn't look so good to create four separate counters like this...
            int theCount = sellersCount.Count();
            int theCount1 = sellersCount1.Count();
            int theCount2 = sellersCount2.Count();
            int theCount3 = sellersCount3.Count();

            //Gonna use this with if-statements below, to run them only once
            //Couldn't make it work if i didn't make one bool for each if/else-statement
            //Would want a better solution though...
            Boolean onlyOnce = false;
            Boolean onlyOnce1 = false;
            Boolean onlyOnce2 = false;
            Boolean onlyOnce3 = false;

            //Using streamwriter to write to file
            using (StreamWriter outputFile = new StreamWriter(filePath, true)) {

                //Loop through the objects and see how many products they have sold
                foreach (var item in sellers)
            {
                if (item.products < 50)
                {
                        //I only want this line to be written once and not for every object that matches condition
                        //I'm sure there is a better solution but I can't come up with it
                        //Also, the line below comes before listing all the sellers, can't figure out how to write it out after listing the sellers
                        if (onlyOnce == false)
                    {
                            outputFile.WriteLine();
                            outputFile.WriteLine($"{theCount} seller/s have reached level 1, 0-49 products");
                        onlyOnce = true;
                    }
                }
                else if (item.products > 49 && item.products < 100)
                {
                    if (onlyOnce1 == false)
                    {
                            outputFile.WriteLine();
                            outputFile.WriteLine($"{theCount1} seller/s have reached level 2, 50-99 products");
                        onlyOnce1 = true;
                    }
                }
                else if (item.products > 99 && item.products < 200)
                {
                    if (onlyOnce2 == false)
                    {
                            outputFile.WriteLine();
                            outputFile.WriteLine($"{theCount2} seller/s have reached level 3, 100-199 products");
                        onlyOnce2 = true;
                    }
                }
                else
                {
                    if (onlyOnce3 == false)
                    {
                            outputFile.WriteLine();
                            outputFile.WriteLine($"{theCount3} seller/s have reached level 4, more than 200 products");
                        onlyOnce3 = true;
                    }
                }
                
                //Write out the info about the seller to the file
                outputFile.WriteLine($" Name: {item.name}, ID: {item.id}, District: {item.district}, Products: {item.products}");               
                }
            }

            //Clear console before showing result
            Console.Clear();

            //Read all the text from the file and write it to the console           
            string text = File.ReadAllText(filePath);
            Console.WriteLine(text);
        }
    }
}
