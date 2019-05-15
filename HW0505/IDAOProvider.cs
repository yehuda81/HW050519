using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW0505
{
    interface IDAOProvider
    {
        List<Customer> GetAllCustomers();
        List<Order> GetAllOrders();
        List<Order> GetAllOrdersByCustomerId(int customerId);
        Order GetOrderById(int orderId);
        Customer GetCustomerById(int customerId);
        bool AddCustomer(Customer c);
        bool RemoveCustomer(int customerId);
        bool UpdateCustomer(Customer c, string newName);
        bool AddOrder(Order o);
        bool RemoveOrder(int orderId);
        bool UpdateOrder(Order o, int price);
        List<OrderCustomer> GetAllOrdersCustomer();

    }
}
