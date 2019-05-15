using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FireSharp;

namespace HW0505
{
    class Program
    {
        
        static void Main(string[] args)
        {
            DAOMSSQLProvider provider = new DAOMSSQLProvider();

            Customer c = new Customer(8,"yehuda", "rosh", 38);
            //Customer c1 = new Customer(5,"sivan", "rosh", 34);
            //provider.AddCustomer(c);
            //provider.AddCustomer(c1);
            //provider.GetAllCustomers();

            //provider.GetCustomerById(4);
            //provider.RemoveCustomer(4);

            //provider.GetAllOrdersByCustomerId(4);
            //provider.GetAllCustomers();
            Order o = new Order(2, 7, 40, 140519);
            //provider.AddOrder(o);
            //provider.RemoveCustomer(7);
            //provider.RemoveOrder(1);
            //provider.GetAllOrdersCustomer();
            IDAOProvider Firebase = new DAOFireBaseProvider();
            try
            {
                //Firebase.AddCustomer(c);
               // Firebase.AddOrder(o);
                // Firebase.RemoveCustomer(1);
                // Firebase.RemoveOrder(1);
                //GetOne();
                //Push();
                //Firebase.UpdateOrder(o,300);
                //Firebase.UpdateCustomer(c,"tom");
                //Firebase.GetAllCustomers();
                //Firebase.GetCustomerById(1);
                //Firebase.GetAllOrders();
                //Firebase.GetAllOrdersByCustomerId(1);
                //Delete();

            }
            catch (Exception)
            {
                Console.WriteLine("Exception");
            }
            
        }   

    }
}
