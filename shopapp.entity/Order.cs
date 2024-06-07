using System.Collections.Generic;

namespace shopapp.entity
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string UsersId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public decimal Total { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); // Relationship with OrderItem
    }
}