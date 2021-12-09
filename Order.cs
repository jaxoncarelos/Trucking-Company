namespace TruckingLogin
{
    public class Order
    {
        public string Address { get; set; }
        public int Weight { get; set; }
        public int Quantity { get; set; }
        
        public int TruckerId { get; set; }
        
        public Order(string address, int weight, int quantity)
        {
            Address = address;
            Weight = weight;
            Quantity = quantity;
        }
    }
}