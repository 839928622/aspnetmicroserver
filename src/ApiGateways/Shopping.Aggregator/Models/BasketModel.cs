namespace Shopping.Aggregator.Models
{
    public class BasketModel
    {
        public BasketModel(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; set; }
        public List<BasketItemExtendedModel> Items { get; set; } = new List<BasketItemExtendedModel>();
        public decimal TotalPrice { get; set; }
    }
}
