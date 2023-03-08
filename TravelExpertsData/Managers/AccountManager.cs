using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExpertsData.Models;

namespace TravelExpertsData.Managers
{
    public static class AccountManager
    {
        private readonly static List<Customer> _customer = new List<Customer>();

        public static Customer Authenticate(TravelExpertsContext db, string username, string password)
        {
            var customer = db.Customers
                .SingleOrDefault(user => user.Username == username
                                      && user.Password == password);
            return customer;
        }

        public static void RegisterCustomer(TravelExpertsContext db, Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
        }
        public static Customer GetCustomer(TravelExpertsContext db, int id)
        {
            Customer custProfile = db.Customers.Find(id);
            return (custProfile);
        }

        public static void UpdateCustomer(TravelExpertsContext db, int id, Customer cust)
        {
            Customer? customer = db.Customers.Find(id);
            if (customer != null)
            {
                // copy over new Customer data
                customer.CustFirstName = cust.CustFirstName;
                customer.CustLastName = cust.CustLastName;
                customer.CustAddress = cust.CustAddress;
                customer.CustCity = cust.CustCity;

                customer.CustProv = cust.CustProv;
                customer.CustPostal = cust.CustPostal;
                customer.CustCountry = cust.CustCountry;
                customer.CustHomePhone = cust.CustHomePhone;

                customer.CustBusPhone = cust.CustBusPhone;
                customer.CustEmail = cust.CustEmail;

                customer.Username = cust.Username;
                customer.Password = cust.Password;

                // no need to call Update; just save changes
                db.SaveChanges();
            }

        }

    }
}
