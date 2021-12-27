namespace Shopping.Aggregator.Models
{
    public record ShoppingModel
    {
       

        public string UserName { get; init; }
        public BasketModel BasketWithProducts { get; init; }
        public IEnumerable<OrderResponseModel> Orders { get; init; }
    }
}
