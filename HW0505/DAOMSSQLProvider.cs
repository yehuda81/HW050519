using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW0505
{
    class DAOMSSQLProvider : IDAOProvider
    {    
        public bool AddCustomer(Customer c)
        {
            using (TargilsicumEntities entities = new TargilsicumEntities())
            {
                try
                {
                    entities.Customer.Add(c);
                    entities.SaveChanges();
                    Console.WriteLine($"Customer {c.NAME} Added successfully");
                    return true;
                }
                catch (Exception)
                {
                    Console.WriteLine($"can't add this customer name {c.NAME}");
                    return false;
                }

            }
        }
        public bool AddOrder(Order o)
        {
            using (TargilsicumEntities entities = new TargilsicumEntities())
            {
                try
                {
                    entities.Order.Add(o);
                    entities.SaveChanges();
                    Console.WriteLine($"Order {o.ID} Added successfully");
                    return true;
                }
                catch (Exception)
                {
                    Console.WriteLine($"can't add this order {o.ID}");
                    return false;
                }
            }
        }

        public List<Customer> GetAllCustomers()
        {
            using (TargilsicumEntities entities = new TargilsicumEntities())
            {
                entities.Customer.ToList().ForEach(c => Console.WriteLine(JsonConvert.SerializeObject(c)));
                return entities.Customer.ToList();                
            }
        }

        public List<Order> GetAllOrders()
        {
            using (TargilsicumEntities entities = new TargilsicumEntities())
            {
                entities.Order.ToList().ForEach(o => Console.WriteLine(JsonConvert.SerializeObject(o)));
                return entities.Order.ToList();
            }
        }

        public List<Order> GetAllOrdersByCustomerId(int customerId)
        {
            using (TargilsicumEntities entities = new TargilsicumEntities())
            {
                entities.Order.Where(o => o.CUSTOMER_ID == customerId).ToList().ForEach(o => Console.WriteLine(JsonConvert.SerializeObject(o)));
                return entities.Order.ToList();
            }
        }

        public List<OrderCustomer> GetAllOrdersCustomer()
        {
            using (TargilsicumEntities entities = new TargilsicumEntities())
            {
                List<OrderCustomer> orders = new List<OrderCustomer>();
                entities.Customer.Join(entities.Order, o => o.ID, c => c.CUSTOMER_ID, (c, o)
                    => new
                    {
                        c.NAME,
                        o.CUSTOMER_ID,
                        o.ID,
                        o.PRICE,
                        o.DATE
                    }).ToList().ForEach(co => orders.Add(new OrderCustomer(co.NAME, co.CUSTOMER_ID, co.ID, co.PRICE, co.DATE)));                
                orders.ForEach(co => Console.WriteLine(JsonConvert.SerializeObject(co)));
                return orders;
            }
        }

        public Customer GetCustomerById(int customerId)
        {
            using (TargilsicumEntities entities = new TargilsicumEntities())
            {
                return entities.Customer.FirstOrDefault(c => c.ID == customerId);
            }  
        }

        public Order GetOrderById(int orderId)
        {
            using (TargilsicumEntities entities = new TargilsicumEntities())
            {
                List<Order> orders = entities.Order.Where(o => o.ID == orderId).ToList();
                return orders[0];
            }
        }

        public bool RemoveCustomer(int customerId)
        {
            using (TargilsicumEntities entities = new TargilsicumEntities())
            {
                try
                {
                    entities.Customer.Remove(entities.Customer.FirstOrDefault(c => c.ID == customerId));                    
                    entities.SaveChanges();
                    Console.WriteLine($"Customer id {customerId} removed successfully");
                    return true;                    
                }
                catch (Exception)
                {
                    Console.WriteLine($"can't remove customer id {customerId}");
                    return false;
                }
            }
        }

        public bool RemoveOrder(int orderId)
        {
            using (TargilsicumEntities entities = new TargilsicumEntities())
            {
                try
                {
                    entities.Order.Remove(entities.Order.FirstOrDefault(o => o.ID == orderId));
                    entities.SaveChanges();
                    Console.WriteLine($"Order id {orderId} removed successfully");
                    return true;
                }
                catch (Exception)
                {
                    Console.WriteLine($"can't remove order id {orderId}");
                    return false;
                }
            }
        }

        public bool UpdateCustomer(Customer c, string newName)
        {
            using (TargilsicumEntities entities = new TargilsicumEntities())
            {
                try
                {
                    entities.Customer.Take(1).FirstOrDefault().NAME = $"{newName}"; 
                    entities.SaveChanges();
                    Console.WriteLine($"Customer name update successfully");
                    return true;
                }
                catch (Exception)
                {
                    Console.WriteLine($"can't update customer");
                    return false;
                }
            }
        }

        public bool UpdateOrder(Order o, int price)
        {
            using (TargilsicumEntities entities = new TargilsicumEntities())
            {
                try
                {
                    entities.Order.Take(1).FirstOrDefault().PRICE = price;
                    entities.SaveChanges();
                    Console.WriteLine($"order price update successfully");
                    return true;
                }
                catch (Exception)
                {
                    Console.WriteLine($"can't update order");
                    return false;
                }
            }
        }
    }
}
