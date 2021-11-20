namespace Shopping.Aggregator.Models
{
    public record OrderResponseModel
    {
        public OrderResponseModel(string userName, decimal totalPrice, string firstName, string lastName, string emailAddress, string addressLine, string country, string state, string zipCode, string cardName, string cardNumber, string expiration, string cVV, int paymentMethod)
        {
            UserName = userName;
            TotalPrice = totalPrice;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            AddressLine = addressLine;
            Country = country;
            State = state;
            ZipCode = zipCode;
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = expiration;
            CVV = cVV;
            PaymentMethod = paymentMethod;
        }

        public string UserName { get; init; }
        public decimal TotalPrice { get; init; }

        // BillingAddress
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string EmailAddress { get; init; }
        public string AddressLine { get; init; }
        public string Country { get; init; }
        public string State { get; init; }
        public string ZipCode { get; init; }

        // Payment
        public string CardName { get; init; }
        public string CardNumber { get; init; }
        public string Expiration { get; init; }
        public string CVV { get; init; }
        public int PaymentMethod { get; init; }
    }
}
