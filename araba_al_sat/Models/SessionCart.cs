using e_commerce.Infastructe.Extensions;
using Entities.Models;
using System.Text.Json.Serialization;

namespace e_commerce.Models
{
    public class SessionCart : Cart
    {
        [JsonIgnore]
        public ISession? Session { get; set; }

        public static Cart GetCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session;

            SessionCart cart = session?.GetJson<SessionCart>("cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }
        public override void AddItem(Products products, int quantity)
        {
            base.AddItem(products, quantity);
            Session?.SetJson<SessionCart>("cart",this);
        }

        public override void Clear()
        {
            base.Clear();
            Session?.Remove("cart");
        }

        public override void RemoveLine(Products products)
        {
            base.RemoveLine(products);
            Session?.SetJson<SessionCart>("cart", this);
        }
        public override void DecreaseQuantity(Products products)
        {
            base.DecreaseQuantity(products);
            Session?.SetJson<SessionCart>("cart", this);
        }
    }
}
