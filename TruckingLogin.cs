using System.Collections.Generic;

namespace TruckingLogin
{
    public class TruckingLogin
    {
        public int id { get; set; }
        public string password { get; set; }
        public string Name { get; set; }
        
        public List<Order> Orders { get; set; }
        
        public TruckingLogin(int id, string password, string name)
        {
            this.id = id;
            this.password = password;
            this.Name = name;

            Orders = new List<Order>();
        }
        public void AddNewOrder(Order order)
        {
            Orders.Add(order);
        }
    }
}