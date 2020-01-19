using System;
using System.Collections;
using System.Collections.Generic;

namespace ClassAssignment
{
    class Program
    {
        public enum Meals
        {
            none = 0,
            appetizer,
            main,
            desert,
            done
        }

        static void Main(string[] args)
        {
            Queue<Customer> customer = CreateCustomerData();

            foreach (Customer cus in customer)
            {
                cus.TableOpenEvent += cus.Handletable;
                cus.ChangeMealEvent += cus.HandleChangeMeal;
                cus.TableOpen(cus.FirstName, cus.LastName);
                int mealCount = Enum.GetNames(typeof(Meals)).Length;
                for (int i = 1; i < mealCount; i++)
                {
                    cus.ChangeMeal(cus.FirstName, cus.LastName, ((Meals)i));
                }
                Console.WriteLine();
            }
            Console.ReadLine();
              
        }

        private static Queue<Customer> CreateCustomerData()
        {
            Queue<Customer> customer = new Queue<Customer>();
            Customer c = new Customer();
            c.FirstName = "Joe";
            c.LastName = "Smith";
            customer.Enqueue(c);

            c = new Customer();
            c.FirstName = "Jane";
            c.LastName = "Jones";
            customer.Enqueue(c);

            c = new Customer();
            c.FirstName = "Jack";
            c.LastName = "Jump";
            customer.Enqueue(c);
            c = new Customer();

            c.FirstName = "Jeff";
            c.LastName = "Run";
            customer.Enqueue(c);

            return customer;
        }

        class Customer
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Meals Meal { get; set; }

            public event ChangeMealEventHandeler ChangeMealEvent;
            public event TableOpenEventHandeler TableOpenEvent;
          
            public void HandleChangeMeal(object sender, ChangeMealEventArgs e)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} is having {e.Meal}");
            }
           
            public void ChangeMeal(string fname, string lname, Meals m)
            {
                ChangeMealEventHandeler change = ChangeMealEvent;
                if (change != null)
                {
                    change(this, new ChangeMealEventArgs(fname, lname, m));
                }
            }

            public void Handletable(object sender, TableOpenEventArgs e)
            {
                Console.WriteLine("Table is open!");
                Console.WriteLine($"{e.FirstName} {e.LastName} got table.");
            }

            public void TableOpen(string fname, string lname)
            {
                TableOpenEventHandeler table = TableOpenEvent;
                if (table != null)
                {
                    table(this, new TableOpenEventArgs(fname, lname));
                }
            }
        }

        public delegate void TableOpenEventHandeler(object source, TableOpenEventArgs e);

        public class TableOpenEventArgs : EventArgs
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public TableOpenEventArgs(string fname, string lname)
            {
                this.FirstName = fname;
                this.LastName = lname;
            }
        }

        public delegate void ChangeMealEventHandeler(object source, ChangeMealEventArgs e);

        public class ChangeMealEventArgs : EventArgs
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Meals Meal { get; set; }
            public ChangeMealEventArgs(string fname, string lname, Meals meal)
            {
                this.Meal = meal;
                this.FirstName = fname;
                this.LastName = lname;
                
            }
        }
    }
}
