namespace GraphQLAgentApp.Models.GraphQL
{
    /// <summary>
    /// GraphQL model for Order exposed in API
    /// </summary>
    public class OrderGraphQLModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? ShippingAddress { get; set; }
        public string? BillingAddress { get; set; }
        public string? Notes { get; set; }
        
        // Navigation properties
        public CustomerGraphQLModel? Customer { get; set; }
        public List<OrderItemGraphQLModel> OrderItems { get; set; } = new List<OrderItemGraphQLModel>();
        
        // GraphQL-specific computed properties
        public string FormattedTotalAmount => $"${TotalAmount:F2}";
        public string OrderDateFormatted => OrderDate.ToString("MMM dd, yyyy");
        public bool IsPending => Status.Equals("Pending", StringComparison.OrdinalIgnoreCase);
        public bool IsCompleted => Status.Equals("Delivered", StringComparison.OrdinalIgnoreCase);
        public int ItemCount => OrderItems.Count;
    }
} 