namespace Shopping.Aggregator.Models
{
    public class BasketItemExtendedModel
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="color"></param>
        /// <param name="price"></param>
        /// <param name="productId"></param>
        /// <param name="productName"></param>
        /// <param name="category"></param>
        /// <param name="summary"></param>
        /// <param name="description"></param>
        /// <param name="imageFile"></param>
        public BasketItemExtendedModel(string color, decimal price, string productId, string productName, string category, string summary, string description, string imageFile)
        {
            Color = color;
            Price = price;
            ProductId = productId;
            ProductName = productName;
            Category = category;
            Summary = summary;
            Description = description;
            ImageFile = imageFile;
        }

        public int Quantity { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }

        //Product Related Additional Fields
        public string Category { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
    }
}
