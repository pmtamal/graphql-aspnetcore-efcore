namespace GraphQLAgentApp.Models.GraphQL
{
    /// <summary>
    /// GraphQL model for OrderItem exposed in API
    /// </summary>
    public class OrderItemGraphQLModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        
        // Navigation properties
        public BookGraphQLModel? Book { get; set; }
        
        // GraphQL-specific computed properties
        public string FormattedUnitPrice => $"${UnitPrice:F2}";
        public string FormattedTotalPrice => $"${TotalPrice:F2}";
        public bool IsDiscounted => UnitPrice > 0 && TotalPrice < (UnitPrice * Quantity);
    }
} 