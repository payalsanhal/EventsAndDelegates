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
            var table = new Table();
            var cusMeal = new ChangeMealOfPerson();
            foreach (Customer cus in customer)
            {
                table.TableOpenEvent += cus.Handletable;
                cusMeal.ChangeMealEvent += cus.HandleChangeMeal;
                table.TableOpen(cus.FirstName, cus.LastName);
                int mealCount = Enum.GetNames(typeof(Meals)).Length;
                for (int i = 1; i < mealCount; i++)
                {
                    cusMeal.ChangeMeal(cus.FirstName, cus.LastName, ((Meals)i));
                }
                table.TableOpenEvent -= cus.Handletable;
                cusMeal.ChangeMealEvent -= cus.HandleChangeMeal;
            }
            Console.WriteLine("Everyone is full!");
            Console.ReadLine();
              
        }

        private static Queue<Customer> CreateCustomerData()
        {
            Queue<Customer> customer = new Queue<Customer>();
            Customer cust = new Customer();
            cust.FirstName = "Joe";
            cust.LastName = "Smith";
            customer.Enqueue(cust);

            cust = new Customer();
            cust.FirstName = "Jane";
            cust.LastName = "Jones";
            customer.Enqueue(cust);

            cust = new Customer();
            cust.FirstName = "Jack";
            cust.LastName = "Jump";
            customer.Enqueue(cust);
           
            cust = new Customer();
            cust.FirstName = "Jeff";
            cust.LastName = "Run";
            customer.Enqueue(cust);

            cust = new Customer();
            cust.FirstName = "Jill";
            cust.LastName = "Hill";
            customer.Enqueue(cust);

            cust = new Customer();
            cust.FirstName = "John";
            cust.LastName = "Winstone";
            customer.Enqueue(cust);

            return customer;
        }

        class Customer
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Meals Meal { get; set; }
          
            public void HandleChangeMeal(object sender, ChangeMealEventArgs e)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} is having {e.Meal}");
            }
           
            public void Handletable(object sender, TableOpenEventArgs e)
            {
                Console.WriteLine("Table is open!");
                Console.WriteLine($"{e.FirstName} {e.LastName} got table.");
            }

            
        }
        public class ChangeMealOfPerson
        {
            public event EventHandler<ChangeMealEventArgs> ChangeMealEvent;
            public void ChangeMeal(string fname, string lname, Meals m)
            {
                //ChangeMealEventHandeler change = ChangeMealEvent;
                if (ChangeMealEvent != null)
                {
                    ChangeMealEvent(this, new ChangeMealEventArgs(fname, lname, m));
                }
            }

        }
        public class Table
        {
            public event EventHandler<TableOpenEventArgs>  TableOpenEvent;
            public void TableOpen(string fname, string lname)
            {
               // TableOpenEventHandeler table = TableOpenEvent;
                if (TableOpenEvent != null)
                {
                    TableOpenEvent(this, new TableOpenEventArgs(fname, lname));
                }
            }
        }

       //public delegate void TableOpenEventHandeler(object source, TableOpenEventArgs e);

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

       // public delegate void ChangeMealEventHandeler(object source, ChangeMealEventArgs e);

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
