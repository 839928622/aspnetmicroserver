using System.Net;
using System.Threading.Tasks;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Models;
using Basket.API.Repositories;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcServices _discountGrpcServices;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketController(IBasketRepository basketRepository, DiscountGrpcServices discountGrpcServices,
                                IPublishEndpoint publishEndpoint)
        {
            _basketRepository = basketRepository;
            _discountGrpcServices = discountGrpcServices;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _basketRepository.GetBasket(userName);

            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.Created)]
        public async Task<ActionResult> UpdateBasket([FromBody] ShoppingCart basket)
        {
             // todo: Communicate with Discount.Grpc
            // and calculate latest prices of product into shopping cart
            foreach (var item in basket.Items)
            {
                var coupon = await _discountGrpcServices.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }


            var createdBasket=  await _basketRepository.UpdateBasket(basket);
            return CreatedAtAction(nameof(GetBasket), createdBasket.UserName);
        }


        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _basketRepository.DeleteBasket(userName);
            return NoContent();
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            // get existing basket with total price

            var basket = await _basketRepository.GetBasket(basketCheckout.UserName);

            if (basket == null)
            {
                return BadRequest("There is nothing in your basket");
            }
            // set total price on basket checkout event message
            // send checkout event to rammitmq
            await _publishEndpoint.Publish(new BasketCheckoutEvent()
            {
                UserName     = basketCheckout.UserName,
                TotalPrice   = basket.TotalPrice ,
                FirstName    = basketCheckout.FirstName ,
                LastName     = basketCheckout.LastName ,
                EmailAddress = basketCheckout.EmailAddress,
                AddressLine  = basketCheckout.AddressLine,
                Country      = basketCheckout.Country ,
                State        = basketCheckout.State ,
                ZipCode      = basketCheckout.ZipCode ,
                CardName     = basketCheckout.CardName ,
                CardNumber   = basketCheckout.CardNumber ,
                Expiration   = basketCheckout.Expiration ,
                CVV          = basketCheckout.CVV ,
                PaymentMethod= basketCheckout.PaymentMethod 
               });
            //remove the basket

            await _basketRepository.DeleteBasket(basket.UserName);

            return Accepted();

        }

    }
}
