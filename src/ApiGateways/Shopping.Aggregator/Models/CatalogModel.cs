namespace Shopping.Aggregator.Models
{
    public record CatalogModel
    {
        public CatalogModel(string id, string name, string category, string summary, string description, string imageFile, decimal price)
        {
            Id = id;
            Name = name;
            Category = category;
            Summary = summary;
            Description = description;
            ImageFile = imageFile;
            Price = price;
        }

        public string Id { get; init; }
        public string Name { get; init; }
        public string Category { get; init; }
        public string Summary { get; init; }
        public string Description { get; init; }
        public string ImageFile { get; init; }
        public decimal Price { get; init; }
    }
}
