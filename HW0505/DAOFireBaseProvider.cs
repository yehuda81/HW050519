using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;

namespace HW0505
{
    class DAOFireBaseProvider : IDAOProvider
    {
        static IFirebaseClient firebaseClient;
        static DAOFireBaseProvider()
        {
            FirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "SD2p5mBeJwejryOl5DDKWVLAcU2HJfx3XA3qWRBz",
                BasePath = "https://hw050519.firebaseio.com/"
            };

            firebaseClient = new FireSharp.FirebaseClient(config);
            if (firebaseClient != null)
            {
                Console.WriteLine("Conection succeeded!");
            }
        }            

        public bool AddCustomer(Customer c)
        {
            FirebaseResponse firebaseResponse = firebaseClient.Get($"Customer/{c.ID}");
            if (firebaseResponse.Body == "null")
            {
                SetResponse response = firebaseClient.Set($"Customer/{c.ID}", c);                
                Console.WriteLine($"Customer Added {c.ID}");
                return true;
            }
            Console.WriteLine("Customer already exist");
            return false;            
            
        }
        
    public bool AddOrder(Order o)
        {
            FirebaseResponse firebaseResponse = firebaseClient.Get($"Order/{o.ID}");            
            if (firebaseResponse.Body == "null")
            {
                SetResponse response = firebaseClient.Set($"Order/{o.ID}", o);                
                Console.WriteLine($"Customer Added {o.ID}");
                return true;
            }
            Console.WriteLine("Order already exist");
            return false;
        }

        public List<Customer> GetAllCustomers()
        {
            FirebaseResponse response = firebaseClient.Get("Customer");            
            List<Customer> customers = response.ResultAs<List<Customer>>();
            customers.RemoveAt(0);
            customers.ForEach(c => Console.WriteLine(JsonConvert.SerializeObject(c)));            
            return customers;
        }

        public List<Order> GetAllOrders()
        {
            FirebaseResponse response = firebaseClient.Get("Order");
            List<Order> orders = response.ResultAs<List<Order>>();
            orders.RemoveAt(0);
            orders.ForEach(o => Console.WriteLine(JsonConvert.SerializeObject(o)));
            return orders;
        }

        public List<Order> GetAllOrdersByCustomerId(int customerId)
        {
            List<Order> allOrders = GetAllOrders();
            List<Order> ordersByCustomerId = new List<Order>();
            foreach (Order o in allOrders)
            {
                if (o.CUSTOMER_ID == customerId)
                {
                    ordersByCustomerId.Add(o);
                    Console.WriteLine(JsonConvert.SerializeObject(o));                }                
            }
            return ordersByCustomerId;
        }

        public List<OrderCustomer> GetAllOrdersCustomer()
        {
            List<Customer> allCustomers = GetAllCustomers();
            List<Order> allOrders = GetAllOrders();

            var result = (
                from Customer in allCustomers
                join Order in allOrders
                on Customer.ID equals Order.CUSTOMER_ID
                select new
                {
                    _ID = Order.ID,
                    _CustomerName = Customer.NAME,
                    _Customer_Id = Customer.ID,
                    _Price = Order.PRICE,
                    _Date = Order.DATE
                }).ToList();
            List<OrderCustomer> allOrdersCustomers = new List<OrderCustomer>();

            foreach (var r in result)
            {
                allOrdersCustomers.Add(new OrderCustomer
                (
                    r._CustomerName,
                    r._Customer_Id,
                    r._Date,
                    r._ID,
                    r._Price
                ));
            }

            return allOrdersCustomers;
        }

        public Customer GetCustomerById(int customerId)
        {
            FirebaseResponse response = firebaseClient.Get($"Customer/{customerId}");
            Customer customer = response.ResultAs<Customer>();
            Console.WriteLine(JsonConvert.SerializeObject(customer));
            return customer;
        }

        public Order GetOrderById(int orderId)
        {
            FirebaseResponse response = firebaseClient.Get($"Order/{orderId}");
            Order order = response.ResultAs<Order>();
            Console.WriteLine(JsonConvert.SerializeObject(order));
            return order;
        }

        public bool RemoveCustomer(int customerId)
        {
            List<Order> orderlist = GetAllOrdersByCustomerId(customerId);
            if (orderlist.Count == 0)
            {
                DeleteResponse response = firebaseClient.Delete($"Customers/{customerId}");
                return true;
            }
            else
            {
                Console.WriteLine("Customer has orders");
                return false;
            }
        }

        public bool RemoveOrder(int orderId)
        {
            try
            {
                FirebaseResponse firebaseResponse = firebaseClient.Get($"Order/{orderId}");
                Order order = firebaseResponse.ResultAs<Order>();
                DeleteResponse response = firebaseClient.Delete($"Order/{orderId}");
                Console.WriteLine($"Order Removed");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Order not Removed");
                return false;
            }            
            
        }

        public bool UpdateCustomer(Customer c, string newName)
        {
            FirebaseResponse firebaseResponse = firebaseClient.Get($"Customer/{c.ID}");
            if (firebaseResponse.Body != "null")
            {
                FirebaseResponse response = firebaseClient.Update($"Customer/{c.ID}", c.NAME = newName);
                Console.WriteLine($"Customer Updated");
                return true;
            }
            Console.WriteLine("Customer not exist");
            return false;
        }

        public bool UpdateOrder(Order o, int price)
        {
            FirebaseResponse firebaseResponse = firebaseClient.Get($"Order/{o.ID}");            
            if (firebaseResponse.Body != "null")
            {
                FirebaseResponse response = firebaseClient.Update($"Order/{o.ID}",o.PRICE = price);
                Console.WriteLine($"Order Updated");
                return true;
            }
            Console.WriteLine("Order not exist");
            return false;
        }
    }
}
