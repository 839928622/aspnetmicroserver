namespace Shopping.Aggregator.Models
{
    public record ShoppingModel
    {
        public ShoppingModel(string userName, BasketModel basketWithProducts, IEnumerable<OrderResponseModel> orders)
        {
            UserName = userName;
            BasketWithProducts = basketWithProducts;
            Orders = orders;
        }

        public string UserName { get; init; }
        public BasketModel BasketWithProducts { get; init; }
        public IEnumerable<OrderResponseModel> Orders { get; init; }
    }
}
