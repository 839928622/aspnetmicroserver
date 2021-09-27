using Ordering.Application.MapperCodeGen;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mapper
{
    public static partial class OrderMapper
    {
        public static OrderDto AdaptToDto(this Order p1)
        {
            return p1 == null ? null : new OrderDto()
            {
                UserName = p1.UserName,
                TotalPrice = p1.TotalPrice,
                FirstName = p1.FirstName,
                LastName = p1.LastName,
                EmailAddress = p1.EmailAddress,
                AddressLine = p1.AddressLine,
                Country = p1.Country,
                State = p1.State,
                ZipCode = p1.ZipCode,
                CardName = p1.CardName,
                CardNumber = p1.CardNumber,
                Expiration = p1.Expiration,
                CVV = p1.CVV,
                PaymentMethod = p1.PaymentMethod,
                Id = p1.Id
            };
        }
        public static OrderDto AdaptTo(this Order p2, OrderDto p3)
        {
            if (p2 == null)
            {
                return null;
            }
            OrderDto result = p3 ?? new OrderDto();
            
            result.UserName = p2.UserName;
            result.TotalPrice = p2.TotalPrice;
            result.FirstName = p2.FirstName;
            result.LastName = p2.LastName;
            result.EmailAddress = p2.EmailAddress;
            result.AddressLine = p2.AddressLine;
            result.Country = p2.Country;
            result.State = p2.State;
            result.ZipCode = p2.ZipCode;
            result.CardName = p2.CardName;
            result.CardNumber = p2.CardNumber;
            result.Expiration = p2.Expiration;
            result.CVV = p2.CVV;
            result.PaymentMethod = p2.PaymentMethod;
            result.Id = p2.Id;
            return result;
            
        }
    }
}