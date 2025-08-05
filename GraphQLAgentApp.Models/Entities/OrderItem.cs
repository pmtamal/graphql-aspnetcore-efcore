namespace GraphQLAgentApp.Models.Entities
{
    /// <summary>
    /// Database entity for OrderItem (individual items in an order)
    /// </summary>
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public virtual Order Order { get; set; } = null!;
        public virtual Book Book { get; set; } = null!;
    }
} 